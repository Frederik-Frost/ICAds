import Layer from './Layer'
export default class ImageLayer extends Layer {
  constructor(layerType, layer) {
    super(layerType)
    this.source = layer.source,
    this.width = layer.width
    this.height = layer.height;
    // fit or cover
    this.objectFit = layer.objectFit;
    this.alignHorizontal = layer.alignHorizontal;
    this.alignVertical = layer.alignVertical;
    this.posX = layer.posX;
    this.posY = layer.posY;

  }
  export() {
    return {
      layerType: super.export(),
      source: this.source,
      width: this.width,
      height: this.height,
      objectFit: this.objectFit,
      alignHorizontal: this.alignHorizontal,
      alignVertical: this.alignVertical,
      posX: this.posX,
      posY: this.posY
    };
  }
}