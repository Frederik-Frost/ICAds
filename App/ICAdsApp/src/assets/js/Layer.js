export default class Layer {
    constructor(layerType, layer){
        this.layerType = layerType
    }


    export () {
        return this.layerType
    }
}