const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const path = require('path');

const directoryName = './public';
const fileName = 'bundle';

module.exports = (env, argv) => {
	return {
		mode: argv.mode === "production" ? "production" : "development",
		entry: ['./src/custom.scss'],
		output: {
			filename: fileName + '.js',
			path: path.resolve(__dirname, directoryName)
		},
		module: {
			rules: [
				{
					test: /\.s[c|a]ss$/,
					use:
						[
							'style-loader',
							MiniCssExtractPlugin.loader,
							'css-loader',
							{
								loader: 'postcss-loader', // Run postcss actions
								options: {
									postcssOptions: {
										plugins: function () { // postcss plugins, can be exported to postcss.config.js

											let plugins = [require('autoprefixer')];

											if (argv.mode === "production") {

												plugins.push(require('cssnano'));
											}

											return plugins;
										}
									}
								}
							},
							'sass-loader'
						]
				}
			]
		},
		plugins: [
			new CleanWebpackPlugin(),
			new MiniCssExtractPlugin({
				filename: fileName + '.css'
			})
		]
	};
};