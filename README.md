# Duotone

![GIF](https://github.com/keijiro/Duotone/assets/343936/e76fddc1-9870-4b14-a192-dd2e2f084ae4)
![GIF](https://github.com/keijiro/Duotone/assets/343936/d1ea1084-cc8e-46ee-adda-0f251e5eee08)

**Duotone** is a simple image stylization effect for Unity URP.

It provides the following two features:

- *Duotone posterization* - It reconstructs an input image with two colors
  (duotone). It also provides two extra peak colors (dark/highlight) and
  dithering options.
- *Contour lines* - It draws contour lines on object edges. To use this mode,
  you must use the Duotone Surface shader to embed normal/depth information
  into the color buffer.

## System requirements

- Unity 2022.3 LTS or later

## How to install

[Follow those instructions] to set up the scoped registry. Then, you can install
the Duotone package via Package Manager.

[Follow those instructions]:
  https://gist.github.com/keijiro/f8c7e8ff29bfe63d86b888901b82644c

## How to set up

First, you must add the Duotone Feature to the URP Renderer Features list.
Please check [the manual page] if you haven't used renderer features with URP.

Unlike the other renderer features, the Duotone Feature doesn't provide any
property. You can control the effect using the Duotone Controller component.

![Inspector](https://github.com/keijiro/Duotone/assets/343936/b4811309-b73b-4adc-8d1d-b7a8635ae7df)

You should add this controller component to cameras to activate the effect.

[the manual page]:
  https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@14.0/manual/urp-renderer-feature.html
