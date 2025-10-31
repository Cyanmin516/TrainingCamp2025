# 虛擬化
* 全部虛擬化
    * VMware Esxi
* 半虛擬化
    * Vbox、VMware、WS
* 容器
    * K8S、docker




## 全部虛擬化
1. 底層 Hardware
2. OS兼vmtool: PVE、HyperV、Esxi
3. vm: Linux、Windos
```
底層OS兼虛擬化工具、最大化虛擬化效能
```

## 半虛擬化
1. 底層 Hardware
2. OS: Windows10、W11、Ubuntu、CentOS
3. tool: Vbox、VMware、WS
4. vm: Linux、Windows
```
Vbox與VMware在同一層，差別不會太多
需要資源給底層管理OS的圖形介面
```
1.1 儲存設備: 去叢壓縮，把每一台重複資訊壓縮。大量部屬時可以省下重複的Kernel

## 容器
1. 底層 Hardware
2. OS: Windows10、W11、Ubuntu、CentOS
3. container
4. vm: Linux、Windows
```
容器直接取用底層OS的重要Kernel，快速啟動服務
kernel
```
