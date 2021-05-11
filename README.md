# VISAInstrument
基于NI-VISA的仪器编程，支持RS232、USB、GPIB及LAN

## 条件
运行或开发软件时，必须安装NI-VISA运行时(其他VISA版本不支持，如Keysight VISA等)。
### Release版本
运行条件：
若需运行在XP及以上系统(最高支持Win10系统)，请安装15.5版本的运行时，下载链接如下：
https://download.ni.com/support/softlib/visa/NI-VISA/15.5/Windows/NIVISA1550runtime.zip

若需运行在Win7及以上系统(最高支持Win10系统)，请安装16.0~18.5版本的运行时，18.5版本的下载链接如下：
https://download.ni.com/support/softlib/visa/NI-VISA/18.5/Windows/NIVISA1850runtime.zip

### 开发版本
为了最佳的开发效果，开发时请使用最新的完整版本(截止目前为止，版本为20.0)，下载链接如下：
https://download.ni.com/support/nipkg/products/ni-v/ni-visa/20.0/offline/ni-visa_20.0.0_offline.iso

注意：安装完整版后开发软件时，可以从GAC中引用完整版对应的Ivi.Visa.dll\NationalInstruments.Common.dll\NationalInstruments.Visa.dll，代替本项目中Library下的库文件。

## RS232
支持常见的RS232串口编程，一般地址为“ASRL1::INSTR”

## USB
支持常见的USB接口编程，一般地址类似为“USB0::0x2A8D::0x0101::MY57501899::INSTR”

## GPIB
支持常见的USB接口编程，一般地址类似为“GPIB0::0x2A8D::0x0101::MY57501899::INSTR”
此处要求安装GPIB卡驱动程序，推荐使用NI-GPIB卡（需要驱动程序，驱动程序可以从NI官方网站下载）

## LAN
支持常见的LAN接口编程，一般地址类似为“TCPIP0::34465A-01899::inst0::INSTR”或“TCPIP0::192.168.0.26::INSTR”

## 运行界面
![image](https://github.com/cnxy/VISAInstrument/blob/master/VISAInstrument/pic.png)
