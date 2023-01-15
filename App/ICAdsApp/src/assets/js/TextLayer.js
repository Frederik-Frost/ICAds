import Layer from './Layer';
export default class TextLayer extends Layer {
  constructor(layerType, layer) {
    super(layerType);
    this.text = layer ? layer.text : 'Insert text';
    this.textSize = layer ? layer.textSize : 20;
    this.textColor = layer ? layer.textColor : '#383838';
    this.fontFamily = layer ? layer.fontFamily : 'Roboto';
    this.fontWeight = layer ? layer.fontWeight : 400;
    this.fontSlant = layer ? layer.fontSlant : 0;
    this.fontWidth = layer ? layer.fontWidth : 5;
    this.posX = layer ? layer.posX : 0;
    this.posY = layer ? layer.posY : 0;
    this.hasBackground = layer ? layer.hasBackground : true;
    this.inflateX = layer ? layer.inflateX : 10;
    this.inflateY = layer ? layer.inflateY : 10;
    this.backgroundInflate = layer ? layer.backgroundInflate : true;
    // Either Stroke or Fill
    this.backgroundStyle = layer ? layer.backgroundStyle : "Fill";
    this.backgroundColor = layer ? layer.backgroundColor : "#ffffff";
    this.borderRadius = layer ? layer.borderRadius : 5;
  }

  export() {
    return {
      layerType: super.export(),
      text: this.text,
      textSize: this.textSize,
      textColor: this.textColor,
      fontFamily: this.fontFamily,
      fontWeight: this.fontWeight,
      fontSlant: this.fontSlant,
      fontWidth: this.fontWidth,
      position: this.position,
      posX: this.posX,
      posY: this.posY,
      hasBackground: this.hasBackground,
      backgroundInflate: this.backgroundInflate,
      inflateX: this.inflateX,
      inflateY: this.inflateY,
      backgroundStyle: this.backgroundStyle,
      backgroundColor: this.backgroundColor,
      borderRadius: this.borderRadius,
    };
  }
}
