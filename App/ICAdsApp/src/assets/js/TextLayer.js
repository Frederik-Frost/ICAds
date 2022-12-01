import Layer from './Layer'
export default class TextLayer extends Layer {
  //   constructor(
  //     layerType,
  //     text,
  //     textSize,
  //     textColor,
  //     fontFamiliy,
  //     PosX,
  //     PosY,
  //     hasBackground,
  //     inflateX,
  //     inflateY,
  //     backgroundStyle,
  //     backgroundColor,
  //     borderRadius
  //   ) {
  constructor(layerType, layer) {
    // Layer width and height
    // this.height = height;
    // this.width = width;
    // text
    //layertype is either ImageLayer, ShapeLayer, TextLayer
    // this.layerType = layer.layerType;
    super(layerType)
    this.text = layer.text;
    this.textSize = layer.textSize;
    this.textColor = layer.textColor;
    this.fontFamiliy = layer.fontFamiliy || 'Roboto';
    this.posX = layer.posX;
    this.posY = layer.posY;
    this.hasBackground = layer.hasBackground;
    this.inflateX = layer.inflateX;
    this.inflateY = layer.inflateY
    this.backgroundInflate = layer.backgroundInflate;
    // Either Stroke or Fill
    this.backgroundStyle = layer.backgroundStyle;
    this.backgroundColor = layer.backgroundColor;
    this.borderRadius = layer.borderRadius;
  }

  export(){
    return {
      layerType: super.export(),
      text: this.text,
      textSize: this.textSize,
      textColor: this.textColor,
      fontFamiliy: this.fontFamiliy,
      position: this.position,
      posX: this.posX,
      posY: this.posY,
      hasBackground: this.hasBackground,
      backgroundInflate: this.backgroundInflate,
      inflateX: this.inflateX,
      inflateY: this.inflateY,
      backgroundStyle: this.backgroundStyle,
      backgroundColor: this.backgroundColor,
      borderRadius: this.borderRadius      
    }
  }
}
