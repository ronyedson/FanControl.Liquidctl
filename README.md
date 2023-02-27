# FanControl.Liquidctl

This is a fork of [SuspiciousActivity](https://github.com/SuspiciousActivity/FanControl.Liquidctl)'s original. It aims to add ability to control fans attached to a [NZXT RGB & Fan Controller](https://nzxt.com/product/rgb-and-fan-controller).

This is a simple plugin that uses [SuspiciousActivity](https://github.com/SuspiciousActivity/FanControl.Liquidctl)'s custom [liquidctl](https://github.com/SuspiciousActivity/liquidctl) to provide sensor data and pump control to variety of AIOs. I've only tested on my computer with only the NZXT controller so it may or may not has issues with other devices on your computer so use it at your onw risk.

## Installation

Grab a release and unpack it to `Plugins` directory of your FanControl instalation. It contains Windows bundle of liquidctl.

## Setting up the developer environment

The project, after being imported to Visual Studio needs to have it reference to `FanControl.Plugins.dll` and `Newtonsoft.Json.dll` from FanControl directory. You also need to create the executable of liquidctl, which can be automatized with script `build-liquidctl.sh`.

## Screenshots

![Fan speed sensor](/docs/images/FanSpeeds.png)
![Fan controls](/docs/images/FanControls.png)

## License
MIT license, because it's superior.
```
Copyright (c) 2022 Jan K. Marucha

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```

liquidctl, which is used by this plugin is provided on [GPLv3](https://github.com/liquidctl/liquidctl/blob/main/LICENSE.txt).
