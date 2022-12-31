export default {
  base64ToImage(base64img, callback) {
    var img = new Image();
    img.onload = function () {
      callback(img);
    };
    img.src = base64img;
  },

  downloadFromBase64(base64, fileName) {
    let link = document.createElement('a');
    link.href = base64;
    link.download = `${fileName}.png`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  },

  downloadZipFromBase64(base64, fileName) {
    let link = document.createElement('a');
    link.href = 'data:text/plain;base64,' +base64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
};
