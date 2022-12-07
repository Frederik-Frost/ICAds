import Layer from './Layer';
import ImageLayer from './ImageLayer';
import TextLayer from './TextLayer';
import ShapeLayer from './ShapeLayer';

export default class Template {
  constructor(template) {
    this.height = template.height || 500 ;
    this.width = template.width || 500;
    this.layers = template.layers ? this.setLayers(template.layers) : [];
  }

  get ratio() {
    return this.height / this.width;
  }

  setLayers(layers) {
    return layers.map((l) => this.createLayertype(l));
  }

  createLayertype(layer) {
    switch (layer.layerType) {
      case 'TextLayer':
        return new TextLayer(layer.layerType, layer);
      case 'ImageLayer':
        return new ImageLayer(layer.layerType, layer);
      case 'ShapeLayer':
        return new ShapeLayer(layer.layerType, layer);
      default:
        return new Layer('Layer', layer);
    }
  }

  export() {
    return {
      height: this.height,
      width: this.width,
      layers: this.layers.map((l) => l.export()),
    };
  }
}
