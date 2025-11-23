import { SlateDescendant, SlateElement, SlateText } from '@wangeditor/editor'

declare module '@wangeditor/editor' {
    // 扩展 Text
    interface SlateText {
        text: string
    }

    // 扩展 Element
    interface SlateElement {
        type: string
        children: SlateDescendant[]
    }
}

declare module '@wangeditor/editor-for-vue' {
    import { Component } from 'vue'
    export const Editor: Component
    export const Toolbar: Component
} 