# do without sudo every time
在 Linux 中，要讓非 `root` 使用者可以執行管理員指令而不需要每次都輸入 `sudo`，主要有兩種方式：

1.  **設定 `sudoers` 檔案：** 這是最常見、最推薦的方法。
2.  **將使用者加入 `root` 群組：** 這種方法**不建議**，因為它賦予了使用者過大的權限，可能帶來安全風險。

-----

### **方法一：設定 `sudoers` 檔案（推薦）**

`sudoers` 檔案是 Linux 系統中用來定義哪些使用者或群組可以使用 `sudo` 指令的設定檔。透過修改此檔案，您可以給予特定使用者或群組執行所有或特定管理員指令的權限，而不需要每次都輸入密碼。

#### **步驟 1: 使用 `visudo` 指令編輯 `sudoers` 檔案**

為了避免語法錯誤導致系統無法使用 `sudo`，**務必使用 `visudo` 指令來編輯**。它會在您儲存時檢查語法。

```bash
sudo visudo
```

#### **步驟 2: 添加使用者或群組**

在開啟的檔案中，找到類似以下這行：

```
root    ALL=(ALL:ALL) ALL
```

這行表示 `root` 使用者擁有完整的權限。您可以參考這行，在它下面添加您的使用者或群組。

**方式 A: 授予特定使用者所有權限（不需密碼）**

如果您想讓使用者 `your_username` 能夠執行所有指令，且**不需要輸入密碼**，可以添加下面這行：

```
your_username ALL=(ALL) NOPASSWD: ALL
```

  * `your_username`：替換成您的使用者名稱。
  * `NOPASSWD: ALL`：表示執行所有指令時都不需要密碼。

**方式 B: 授予特定使用者所有權限（需密碼）**

如果您希望使用者在第一次執行 `sudo` 時仍需輸入密碼，但之後的一段時間內不需要，可以添加下面這行：

```
your_username ALL=(ALL) ALL
```

**方式 C: 授予群組所有權限（需密碼）**

在許多發行版中，例如 Ubuntu，會將系統管理員加入 `sudo` 或 `wheel` 群組。您只要將您的使用者加入這些群組即可。找到以下這行：

```
# Allow members of group sudo to execute any command
%sudo   ALL=(ALL:ALL) ALL
```

這行表示 `sudo` 群組的成員擁有所有權限。只要將您的使用者加入 `sudo` 群組即可：

```bash
sudo usermod -aG sudo your_username
```

**提示：**

  * `%sudo` 表示 `sudo` 群組。
  * `usermod` 指令用來修改使用者帳戶。
  * `-aG` 參數表示將使用者加入（append）到指定的群組（Group）。

完成編輯後，儲存並退出 `visudo`。此後，該使用者就可以使用 `sudo` 指令了。

-----

### **方法二：將使用者加入 `root` 群組（不推薦）**

將使用者直接加入 `root` 群組會賦予他與 `root` 使用者相同的權限，這是一個**重大的安全漏洞**。因為一旦該使用者的帳號被盜，攻擊者將獲得完整的系統控制權。

**不推薦這樣做。** 但若您了解其風險且堅持要執行，指令如下：

```bash
sudo usermod -aG root your_username
```

-----

### **總結**

  * **最安全且推薦的方法**是透過 **`visudo` 編輯 `/etc/sudoers` 檔案**，並設定 `NOPASSWD` 選項，來讓特定使用者或群組可以不輸入密碼就使用 `sudo`。
  * `visudo` 會自動檢查語法，避免您犯錯。
  * **強烈不建議**將使用者加入 `root` 群組，因為這會帶來嚴重資安風險。

您的 Linux 發行版是哪一種？通常 Ubuntu 預設就會有 `sudo` 群組，讓管理變得更方便。





查看 所有USERS
```sh
cat /etc/passwd
使用者編號從1000開始
資料會在/bin/bash
peter3:x:1001:1001::/home/peter3:/bin/bash
真正的密碼資訊存在於只有 root 才能存取的 /etc/shadow 檔案中。
```
| 檔案/目錄	| 用途 |	舉例 |	優點 |
|:-|:-|:-|:-|
| .conf |	單一配置檔，所有設定集中於此 |	httpd.conf |	集中管理，簡單直觀 |
| .d |	模組化配置目錄，包含多個小配置檔 |	nginx/conf.d/ |	模組化，易於擴展和維護 |
| d.conf |	一個具體的配置檔案，通常是某個服務的配置模組 |	journald.conf	| 命名慣例，表示屬於某個服務 |

更改 root password
sudo passwd root



