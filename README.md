# VISAInstrument
基于NI-VISA的仪器编程，支持RS232、USB、GPIB及LAN

## 条件
运行或开发软件时，必须安装NI-VISA运行时(其他VISA版本不支持，如Keysight VISA等)。
### Release版本
运行条件：
若需运行在Win7及以上系统(最高支持Win11系统)，请安装15.5版本或以上的运行时，下载链接如下：
https://download.ni.com/support/softlib/visa/NI-VISA/15.5/Windows/NIVISA1550runtime.zip

若需运行在Win7及以上系统(最高支持Win11系统)，请安装16.0~21.5版本的运行时，18.5版本的下载链接如下：
https://download.ni.com/support/softlib/visa/NI-VISA/18.5/Windows/NIVISA1850runtime.zip

### 开发版本
为了最佳的开发效果，开发时请使用本软件的对应的开发版本(21.0.0)[目前最新版本为21.5]，下载链接如下：
https://download.ni.com/support/nipkg/products/ni-v/ni-visa/21.0/offline/ni-visa_21.0.0_offline.iso

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
