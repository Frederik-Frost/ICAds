import Layer from './Layer';
export default class ShapeLayer extends Layer {
  constructor(layerType, layer) {
    super(layerType);
    this.width = layer ? layer.width : 0;
    this.height = layer ? layer.height : 0;
    this.backgroundColor = layer ? layer.backgroundColor : "#ffffff";
    this.backgroundStyle = layer ? layer.backgroundStyle : "Fill";
    this.borderRadius = layer ? layer.borderRadius : 0;
    this.posX = layer ? layer.posX : 0;
    this.posY = layer ? layer.posY : 0;
  }


  export() {
    return {
      layerType: super.export(),
      width: this.width,
      height: this.height,
      backgroundColor: this.backgroundColor,
      backgroundStyle: this.backgroundStyle,
      borderRadius: this.borderRadius,
      posX: this.posX,
      posY: this.posY
    };
  }
}
