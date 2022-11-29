// as of guide from https://dev.to/erefor/vue-3-tip-detect-click-outside-4e4k


import {onBeforeUnmount, onMounted} from 'vue'

export default function useDetectOutsideClick(component, callback) {
    if (!component) return
    const listener = (event) => {
        if (event.target !== component.value && event.composedPath().includes(component.value)) {
            return
        }
        if (typeof callback === 'function') {
            callback()
        }
    }
    onMounted(() => { window.addEventListener('click', listener) })
    onBeforeUnmount(() => {window.removeEventListener('click', listener)})

    return {listener}
}