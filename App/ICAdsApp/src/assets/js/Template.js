import Layer from './Layer';
import ImageLayer from './ImageLayer';
import TextLayer from './TextLayer';

export default class Template {
  constructor(template) {
    this.height = template.height;
    this.width = template.width;
    this.layers = this.setLayers(template.layers);
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
      default:
        return new Layer('Layer', layer);
    }
  }

  export () {
    return { 
        height: this.height,
        width: this.width,
        layers: this.layers.map(l => l.export())
    }
}
}


