var pixelbox = require('pixelbox');
var fs       = require('fs-extra');
var path     = require('path');

pixelbox.on('tools/saveMaps', function (str) {
	fs.writeFileSync(path.join(process.cwd(), '../Assets/Resources/Data/maps.json'), str);
});
