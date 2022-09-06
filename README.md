# DeleteNetworkX

## 说明

一键删除因手机USB网络共享给电脑后自动生成的网络号, 强迫症专用

运行后会弹出UAC权限申请, 授权后会自动删除注册表:

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkList\Profiles
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkList\Signatures\Unmanaged

子项目中Description包含"网络"关键词的都会被删除

## 拓展

手机开发者选项中可以设置USB插入后的自动行为, 可以实现插入USB后自动开启网络共享

同样, 若是有root权限, Shell命令也可以打开USB网络共享:

```shell
svc usb setFunctions rndis
```

配合Tasker或者XposedEdge使用