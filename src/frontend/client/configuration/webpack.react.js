const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');

const baseDir = path.resolve(__dirname, '../');

module.exports = {
  mode: 'development',
  entry: './src/renderer.tsx',
  target: 'electron-renderer',
  devtool: 'source-map',
  devServer: {
    contentBase: path.join(baseDir, 'dist/renderer.js'),
    compress: true,
    port: 9000
  },
  resolve: {
    alias: {
      ['@']: path.resolve(baseDir, 'src')
    },
    extensions: ['.tsx', '.ts', '.js'],
  },
  module: {
    rules: [
      {
        test: /\.ts(x?)$/,
        include: /src/,
        use: [{ loader: 'ts-loader' }]
      },
      {
        test: /\.s[ac]ss$/i,
        use: [
          'style-loader',
          'css-loader',
          'sass-loader',
        ],
      }
    ]
  },
  output: {
    path: baseDir + '/dist',
    filename: 'src/renderer.js'
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: 'src/index.html'
    })
  ]
};