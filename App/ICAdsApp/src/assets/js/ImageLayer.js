import Layer from './Layer'
export default class ImageLayer extends Layer {
  constructor(layerType, layer) {
    super(layerType)
    this.source = layer.source,
    this.imageWidth = layer.imageWidth
    this.imageHeight = layer.imageHeight;
    // fit or cover
    this.objectFit = layer.objectFit;
    this.alignHorizontal = layer.alignHorizontal;
    this.alignVertical = layer.alignVertical;

  }
  export() {
    return {
      layerType: super.export(),
      source: this.source,
      imageWidth: this.imageWidth,
      imageHeight: this.imageHeight,
      objectFit: this.objectFit,
      alignHorizontal: this.alignHorizontal,
      alignVertical: this.alignVertical,
    };
  }
}