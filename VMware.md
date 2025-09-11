# VMware
模擬作業系統並視窗化雙向管理

## 建立虛擬機
1. file > new virtual machine
2. typical > iso (fetch iso from host path file)
3. OS type choose
4. rename and locate it's new path
5. set vm size  50 GB, save as a single file
6. custom hardware setting: memory 2 GB, process 2, Network NAT, new CD/DVD (iso)

## 橋接網卡
1. edit > virtual network editor...
2. change setting (administrator privileges
3. Add Network > select a netwokr to add: VMnet0
4. choose Bridge and choose NIC `Realtek PCIe GbE Family Controller`
5. setting of vm, network Adapter: birdge(if more then 1 choose the custom one)
6. don't forget check connect status in setting



## 啟動虛擬機
啟動後立刻，滑鼠點進虛擬機視窗
**wait for CD/DVD rom 時 必須要按鍵盤空白建**
進入Windows系統安裝流程




## 修改環境
添加設備須將VM關機
增加hard disk
增加serial port 

修改網路:NAT, segment

## 備份
1. **快照**
快照虛擬機還原檔案與設定

2. **clone new file**
注意如果開啟AD等服務需要複製完整一個檔案


3. 複製虛擬機需要sysrepo 重設硬體ID才不會發生硬體衝突