# SSH in termanal
使用終端機（Terminal）來操作 SSH（Secure Shell）是一種安全且常見的遠端連線方式，尤其在管理伺服器時非常有用。以下是基本的 SSH 指令與使用方式。

### 基本 SSH 指令

SSH 的基本語法是：

`ssh [使用者名稱]@[主機名稱或IP位址]`

例如，如果您想以使用者名稱 `user` 連線到 IP 位址為 `192.168.1.100` 的伺服器，您需要輸入：

`ssh user@192.168.1.100`

---

### 常見的使用情境

#### 1. 連線到遠端伺服器

當您在終端機輸入上述指令並按下 `Enter` 後，系統會提示您輸入密碼。輸入完成後，如果您看到類似 `user@server-name:~$` 的提示符號，代表您已經成功連線到遠端伺服器，可以開始執行指令了。

#### 2. 指定連接埠（Port）

如果您的 SSH 服務沒有使用預設的連接埠 `22`，您需要使用 `-p` 參數來指定。

`ssh -p [連接埠號碼] [使用者名稱]@[主機名稱或IP位址]`

例如，如果 SSH 連接埠是 `2222`，指令會是：

`ssh -p 2222 user@192.168.1.100`

#### 3. 執行單一遠端指令

如果您只想在遠端伺服器上執行一個指令，而不是建立完整的互動式連線，您可以在連線指令後面加上要執行的指令。

`ssh [使用者名稱]@[主機名稱或IP位址] '[指令]'`

例如，想在遠端伺服器上查看 `home` 資料夾內容，可以輸入：

`ssh user@192.168.1.100 'ls /home'`

#### 4. 斷開連線

當您完成操作後，要安全地斷開 SSH 連線，只需在終端機輸入：

`exit`

或者直接使用鍵盤快速鍵 `Ctrl + D`。

---

### 第一次連線的提示

當您第一次連線到一個新的伺服器時，系統會顯示一個警告，詢問您是否信任該主機。

`The authenticity of host '192.168.1.100 (192.168.1.100)' can't be established.`
`ECDSA key fingerprint is SHA256:xxxxxxxxxxxxxxxxxxxxxx.`
`Are you sure you want to continue connecting (yes/no/[fingerprint])?`

這是正常的安全提示。如果您確定該伺服器是您要連線的，請輸入 `yes`，然後按下 `Enter`。此後，該伺服器的主機金鑰（Host Key）會被儲存在您的電腦中，未來再次連線就不會出現此提示。





old version
```sh
ssh -l <username> <remoteIP>
```

## login with key
using public login without typing password

**local host setting** :
1. key generate:
```bash
生產金鑰
ssh-keygen -t rsa -b 4096
<-t 指定類型rsa> <-b 金鑰長度4096>

系統會提示您：
Enter file in which to save the key (/home/user/.ssh/id_rsa):
直接按 Enter 接受預設路徑 (~/.ssh/id_rsa)。
Enter passphrase (empty for no passphrase):
您可以輸入一個密碼來保護您的私鑰（推薦，增加安全性）。
如果不想要密碼，直接按 Enter。
Enter same passphrase again:
再次輸入密碼或直接按 Enter。

完成後，您會在 ~/.ssh/ 目錄下看到兩個檔案：
id_rsa: 私鑰 (Private Key)。非常重要！請務必妥善保管，絕不能分享給任何人。
id_rsa.pub: 公鑰 (Public Key)。這個檔案將會被複製到遠端伺服器。
```

**sshd congig**
```bash
vim /etc/ssh/sshd_config
修改設定
# 密碼登入
PasswordAuthentication = yes 
# 啟用公鑰認證
PubkeyAuthentication yes
# 允許 Root 帳號直接登入
PermitRootLogin yes


參數,更安全的建議值,說明
#禁用密碼登入，完全依賴更安全的金鑰認證。
PasswordAuthentication no
#禁止 Root 密碼登入，但仍允許 Root 使用金鑰登入（如果金鑰已設定）。
PermitRootLogin prohibit-password
```

更新設定並驗證
```
sudo systemctl restart sshd
ssh root@remote_host_ip
```


**remote host**
```bash
ssh-copy-id username@remote_host_ip
continue connecting(yes/no[fingerprint])? yes
username@remote_host_ip's password: 登入密碼
```



 允許新的連接埠 (假設您改為 2222/tcp)
```bash
sudo firewall-cmd --permanent --zone=public --add-port=2222/tcp
# 移除舊的 22 port (如果您確定要移除)
# sudo firewall-cmd --permanent --zone=public --remove-service=ssh
# 重新載入防火牆規則
sudo firewall-cmd --reload
```

**在您重新啟動 sshd 服務後，請勿關閉目前的連線視窗！**
```bash
您應該開啟一個新的終端機視窗，並使用您更改後的新設定（新的 Port 和金鑰認證）來測試連線是否成功。
ssh -p 2222 your_username@remote_host_ip
```