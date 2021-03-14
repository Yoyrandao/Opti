using System;
using System.Net.Sockets;

using CommonTypes.Constants;

using EnsureThat;

using Npgsql;

using Serilog;

using SyncGateway.Contracts.Out;

using Utils.Http;

namespace SyncGateway.Exceptions.Shields
{
    public class ApiExceptionShield : IExceptionShield<ApiResponse>
    {
        #region Implementation of IExceptionShield

        public ApiResponse Protect(Func<ApiResponse> func)
        {
            EnsureArg.IsNotNull(func);

            try
            {
                return func();
            }
            catch (UserFolderNotCreatedException ex)
            {
                _logger.Error($"Cannot create folder for user {ex.Username}");
                
                return new ApiResponse
                {
                    Error = new ResponseError
                    {
                        Message = "User folder not been created", ErrorCode = ErrorCode.FolderNotCreated
                    }
                };
            }
            catch (UserNotInDatabaseException ex)
            {
                _logger.Error($"Cannot create folder for user {ex.Username}");

                return new ApiResponse
                {
                    Error = new ResponseError
                    {
                        Message = "User has been not appeared in database",
                        ErrorCode = ErrorCode.UserHasBeenNotRegistered
                    }
                };
            }
            catch (NpgsqlException e)
            {
                if (e.InnerException?.GetType() == typeof(SocketException))
                {
                    return new ApiResponse
                    {
                        Error = new ResponseError
                        {
                            Message = "Database is not accessible", ErrorCode = ErrorCode.DatabaseNotAccessible
                        }
                    };
                }

                return new ApiResponse
                {
                    Error = new ResponseError
                    {
                        Message = $"Something wrong with sql execution. {e.Message}",
                        ErrorCode = ErrorCode.SqlStatementError
                    }
                };
            }
            catch (StorageNotAccessibleException)
            {
                return new ApiResponse
                {
                    Error = new ResponseError
                    {
                        Message = "Cannot connect to storage. Try again later",
                        ErrorCode = ErrorCode.StorageNotAccessible
                    }
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    Error = new ResponseError
                    {
                        Message = $"Unexpected error: {e}", ErrorCode = ErrorCode.UnexpectedError
                    }
                };
            }
        }

        #endregion

        private readonly ILogger _logger = Log.ForContext<ApiExceptionShield>();
    }
}
