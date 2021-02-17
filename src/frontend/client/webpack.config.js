const electronConfig = require('./configuration/webpack.electron');
const reactConfig = require('./configuration/webpack.react');

module.exports = [
  electronConfig,
  reactConfig
];