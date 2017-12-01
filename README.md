# VISAInstrument
基于NI-VISA的仪器编程，支持RS232、USB、GPIB及LAN
必须安装VISA运行时，下载链接如下：
http://search.ni.com/nisearch/app/main/p/bot/no/ap/tech/lang/zhs/pg/1/sn/catnav:du/q/VISA/

或者到
https://github.com/cnxy/VISAInstrument/releases/tag/1.0.0.0
下载"NIVISA1700runtime.exe"以便安装VISA运行时，
如想安装最新版本，请从第一个链接下载

## RS232
支持常见的RS232串口编程，一般地址为“ASRL1::INSTR”

## USB
支持常见的USB接口编程，一般地址类似为“USB0::0x2A8D::0x0101::MY57501899::INSTR”

## GPIB
支持常见的USB接口编程，一般地址类似为“GPIB0::0x2A8D::0x0101::MY57501899::INSTR”
此处要求安装GPIB卡驱动程序，推荐使用NI-GPIB卡（需要驱动程序，驱动程序可以从NI官方网站下载）

## LAN
支持常见的USB接口编程，一般地址类似为“TCPIP0::34465A-01899::inst0::INSTR”或“TCPIP0::192.168.0.26::INSTR”

### 运行界面
![image](https://github.com/cnxy/VISAInstrument/blob/master/VISAInstrument/pic.png)
