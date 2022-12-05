import Layer from './Layer';
export default class ShapeLayer extends Layer {
  constructor(layerType, layer) {
    super(layerType);
    this.shapeWidth = layer ? layer.shapeWidth : 0;
    this.shapeHeight = layer ? layer.shapeHeight : 0;
    this.backgroundColor = layer ? layer.backgroundColor : "#ffffff";
    this.backgroundStyle = layer ? layer.backgroundStyle : "Fill";
    this.borderRadius = layer ? layer.borderRadius : 0;
  }


  export() {
    return {
      layerType: super.export(),
      shapeWidth: this.shapeWidth,
      shapeHeight: this.shapeHeight,
      backgroundColor: this.backgroundColor,
      backgroundStyle: this.backgroundStyle,
      borderRadius: this.borderRadius,
    };
  }
}
