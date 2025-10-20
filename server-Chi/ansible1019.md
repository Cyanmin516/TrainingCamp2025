# Ansible 
Improve IT skills and contorl hosts with scripts.

## 0. SSH
SSH remote request 

set up two hosts ssh-copy-id login, using public-key login with once password only.

first. sshd for target
vim /etc/ssh/sshd_config
```bash
#line 40
PermitRootLogin yes
#line 45, prohibit-password using key login only, please change to yes for typing
 ubkeyAuthentication yes
#line 65
PasswordAuthentication yes
```
一定要重新啟動SSHD更新
systemctl restart sshd

then 
ssh-copy-id root@IP_yourhost
<yourrootpasswordforAuth>
yes/no[footprints]? <yes>

do these to all your target (self in first)

get all SSH publick key Auth for keeping going Ansible lesson
至少完成本機 ssh-copy-id root@localhost



## 1. Ansible
### 控制工具與名詞
The machine from which you run the Ansible CLI tools (**ansible-playbook** , **ansible**, **ansible-vault** and others). 

**Mangaged nodes**: also referred to as `hosts`,  there are the target devices(servers, network apps or any computer) you aim to mange with Ansible.

**Inventory** : list source. Informate each node, like IP address and groups. 

**Plays** : The main context for Ansible execution. The Play contains variables, roles and odered lists of tasks and can be run repeatedly. Can map hosts and iterate over them.

**Roles** : A limited distribution of reusable Ansible content (tasks, handlers, variables, plugins, templates and files) for use inside of a Play.

**Tasks** : The definition of an ‘action’ to be applied to the managed host. You can execute a single task once with an ad hoc command using `ansible` or `ansible-console` (both create a virtual Play).

**Handlers** : A special form of a Task, that only executes when notified by a previous task which resulted in a `changed` status.

**Modules** : The code and binaries that Ansible copies and excutes on each managed node to accomplish the action defined in each Task. Can be an administrator manage VLAN interfaces on devices

**Plugins** : planning how to connnect to a managed node. Contorl displayed in the console(callback plugins).

**Collecitons** : Formats for playbooks, roles, modules, plugins. Install throuth `Ansible Galaxy`.

### first
remote control hosts tools, installing necessary packages

dnf install ansible-collection-community-general ansible-core ansible-freeipa -y

vim /usr/ansible/inventory.ini
```bash
[myhosts]
host1
host2
```

vim /usr/ansible/playbook.yaml
```bash
- name: My first play
  hosts: myhosts
  tasks:
   - name: Ping my hosts
     ansible.builtin.ping:

   - name: Print message
     ansible.builtin.debug:
       msg: Hello world                                  
```
每個階層有嚴格的縮排要求
```
tasks:
  - name:
task下一層至少兩個空白
```

ansible-playbook -i inventory.ini playbook.yaml
```
PLAY RECAP *********
localhost: ok=3
這樣就是成功
```

### playbook
An Execution Environment image contains the following packages as standard: 


vim /usr/ansible/centos_setup.yml
```yaml 
---
- name: 設定多台 CentOS 主機
  hosts: centos_hosts
  become: true
  gather_facts: true

  vars:
    base_packages:
      - bind
      - bind-chroot
      - bind-utils
      - mariadb
      - mariadb-server
      - php-mysqlnd
      - php-pdo
      - php
      - php-fpm
      - nginx

  tasks:
    - name: 安裝所有套件
      dnf:
        name: "{{ base_packages }}"
        state: present
        update_cache: yes

    - name: 確保設定檔目錄存在
      file:
        path: /usr/ansible
        state: directory
        mode: '0755'

    - name: 備份舊的設定檔（如果存在）
      command: mv /usr/ansible/playbook.yaml /usr/ansible/playbook.yaml.bak
      args:
        removes: /usr/ansible/playbook.yaml.bak
        creates: /usr/ansible/playbook.yaml
      ignore_errors: yes  # 若舊設定檔不存在，忽略錯誤

    - name: 複製新的設定檔
      copy:
        src: inventory.ini
        dest: /usr/ansible/myhome.ini
        owner: root
        group: root
        mode: '0644'

    - name: 修改設定檔內容 - 確保 ENABLE_FEATURE=true 存在
      lineinfile:
        path: /etc/myapp/myapp.conf
        regexp: '^ENABLE_FEATURE='
        line: 'ENABLE_FEATURE=true'
        create: yes
    
    - name: 下載檔案 
      get_url:
        url: "{{ item.url }}"
        dest: "{{ item.dest }}"
      loop:
        - { url: "https://tw.wordpress.org/latest-zh_TW.zip",dest: "/usr/share/nginx/html/latest-zh_TW.zip" }
        - { url: "https://files.phpmyadmin.net/phpMyAdmin/5.2.2/phpMyAdmin-5.2.2-all-languages.zip",dest: "/usr/share/nginx/html/phpMyAdmin-5.2.2-all-languages.zip" }
```








# chatGPT 1 正確的 Ansible 寫法（批次安裝）
不需要每個 package 都分開寫。你可以在 Ansible 的 `dnf` 模組中，一次安裝多個套件，就像你在命令列用 `dnf install ...` 一樣。

---

## ✅ 正確的 Ansible 寫法（批次安裝）

以下是你提供的這串命令：

```bash
dnf install bind bind-chroot bind-utils mariadb mariadb-server php-mysqlnd php-pdo php php php-fpm nginx -y
```

對應到 Ansible Playbook 可寫成：

```yaml
- name: 安裝多個套件
  dnf:
    name:
      - bind
      - bind-chroot
      - bind-utils
      - mariadb
      - mariadb-server
      - php-mysqlnd
      - php-pdo
      - php
      - php-fpm
      - nginx
    state: present
    update_cache: yes
```

---

### 🔍 注意事項

* `dnf` 模組內的 `name` 可以是 **單一字串**（安裝一個套件），也可以是 **list（列表）**，一次安裝多個套件。
* `state: present` 表示「確保這些套件有安裝」
* `update_cache: yes` 等同於 `dnf makecache`，建議放一次即可
* 重複寫多次 `php` 沒意義（會自動去重複）

---

## 🚫 不建議寫法（每個 package 一個 task）

雖然也能這樣寫，但不必要而且會慢很多：

```yaml
- name: 安裝 bind
  dnf: name=bind state=present

- name: 安裝 bind-chroot
  dnf: name=bind-chroot state=present

# ...一直重複下去
```

---

## 🧠 小技巧：使用變數來管理套件清單

如果你有很多套件，建議用變數集中管理：

```yaml
vars:
  base_packages:
    - bind
    - bind-chroot
    - bind-utils
    - mariadb
    - mariadb-server
    - php-mysqlnd
    - php-pdo
    - php
    - php-fpm
    - nginx

tasks:
  - name: 安裝所有套件
    dnf:
      name: "{{ base_packages }}"
      state: present
      update_cache: yes
```

這樣以後要增刪套件也更方便、清晰。

---

需要我幫你把整個 Playbook 整合起來嗎？還是你想加上安裝完後自動啟動服務（像 mariadb、nginx）？

# ChatGPT 1.1 範例：下載多個檔案並指定不同目的地
如果你需要 **下載多個 URL** 並且 **指定目的地**，你可以使用 Ansible 的 `get_url` 模組來處理每一個 URL。這可以讓你一次處理多個下載，並且為每個檔案指定不同的目的地。

### 🔑 方法：

你可以使用 **`loop`** 來對多個 URL 進行迴圈處理，並且為每個檔案指定不同的儲存目的地。

---

### ✅ 範例：下載多個檔案並指定不同目的地

假設你有以下 3 個檔案需要下載：

1. `https://example.com/file1.tar.gz` 下載到 `/tmp/file1.tar.gz`
2. `https://example.com/file2.zip` 下載到 `/tmp/file2.zip`
3. `https://example.com/file3.iso` 下載到 `/tmp/file3.iso`

可以這樣寫：

```yaml
- name: 下載多個檔案到指定目的地
  hosts: all
  become: true
  tasks:
    - name: 下載檔案
      get_url:
        url: "{{ item.url }}"
        dest: "{{ item.dest }}"
      loop:
        - { url: "https://example.com/file1.tar.gz", dest: "/tmp/file1.tar.gz" }
        - { url: "https://example.com/file2.zip", dest: "/tmp/file2.zip" }
        - { url: "https://example.com/file3.iso", dest: "/tmp/file3.iso" }
```

---

### 🔍 解釋：

* `loop` 是一個循環操作，會依次處理 `url` 和 `dest` 配對。
* `get_url` 模組會下載每一個指定的 URL 並儲存到指定的目的地。
* `url` 是檔案的下載 URL。
* `dest` 是下載檔案後儲存的目的地路徑。

### 🧑‍💻 執行 Playbook

1. 儲存這個 Playbook 為 `download_files.yml`
2. 使用下面的命令執行：

```bash
ansible-playbook -i inventory.ini download_files.yml
```

---

### 🔑 可變化的寫法：從變數中讀取 URL 和目的地

如果你有很多 URL 需要處理，可以把 URL 和目的地配置成變數列表，讓 Playbook 更加靈活。

```yaml
- name: 下載多個檔案到指定目的地
  hosts: all
  become: true
  vars:
    files_to_download:
      - { url: "https://example.com/file1.tar.gz", dest: "/tmp/file1.tar.gz" }
      - { url: "https://example.com/file2.zip", dest: "/tmp/file2.zip" }
      - { url: "https://example.com/file3.iso", dest: "/tmp/file3.iso" }

  tasks:
    - name: 下載檔案
      get_url:
        url: "{{ item.url }}"
        dest: "{{ item.dest }}"
      loop: "{{ files_to_download }}"
```

這樣你就可以更靈活地控制下載的檔案，並且維護起來會更簡單。

---

### 🎯 注意事項

* 確保目標目錄存在，否則下載會失敗。如果需要，事先使用 `file` 模組創建目錄。

  ```yaml
  - name: 確保目錄存在
    file:
      path: "/tmp"
      state: directory
      mode: '0755'
  ```

---

如果你有更多的下載需求，或需要進一步處理檔案（例如解壓縮、檢查檔案完整性等），可以繼續擴展 Playbook！需要這方面的幫助嗎？

# chatGPT 2  設定順序
是的，當然！如果某個目錄是由某個服務（如 Nginx、MariaDB 等）創建的，那麼你必須先啟動並啟用該服務，等服務啟動後再進行對應的檔案操作或目錄處理。因此，這個任務確實應該往 **後面** 放，以確保目錄在該服務啟動並創建相關檔案後才出現。

---

### ✅ 設定順序

1. **啟動服務**：首先啟動服務，例如 Nginx 或 MariaDB，這樣相關目錄才能在服務啟動後正確生成。
2. **檢查並確保目錄存在**：接著確認目錄是否已經存在，如果不存在，可以使用 `file` 模組來創建目錄。
3. **執行後續任務**：完成後續的檔案下載、設定檔修改等任務。

---

### ✅ 範例 Playbook 設定

這是一個簡單的例子，展示如何先啟動服務，然後再處理目錄和檔案。

```yaml
- name: 安裝並設定 Nginx 和下載檔案
  hosts: all
  become: true
  tasks:

    # 安裝 Nginx
    - name: 安裝 Nginx
      dnf:
        name: nginx
        state: present
        update_cache: yes

    # 啟動並啟用 Nginx 服務
    - name: 啟動並啟用 Nginx 服務
      systemd:
        name: nginx
        state: started
        enabled: yes

    # 等待服務啟動並創建必要的目錄（例如 Nginx 的 web 根目錄）
    - name: 等待 Nginx 服務啟動
      wait_for:
        host: "{{ inventory_hostname }}"
        port: 80
        timeout: 60
        state: started

    # 確保目錄存在
    - name: 確保 Nginx 根目錄存在
      file:
        path: "/usr/share/nginx/html"
        state: directory
        mode: '0755'

    # 下載檔案（例如下載 Nginx 的預設網頁）
    - name: 下載預設網頁
      get_url:
        url: https://nginx.org/download/nginx-1.20.1.tar.gz
        dest: /tmp/nginx-1.20.1.tar.gz

    # 其他任務（如果有的話）
```

---

### 🔍 解釋

1. **安裝 Nginx**：使用 `dnf` 安裝 Nginx。
2. **啟動並啟用服務**：用 `systemd` 模組啟動並設置 Nginx 開機自啟動。
3. **等待服務啟動**：用 `wait_for` 等待 Nginx 在端口 80 上啟動，確保相關目錄被服務創建或初始化。
4. **確認目錄存在**：使用 `file` 模組確保目錄存在（這裡以 `/usr/share/nginx/html` 作為範例目錄）。
5. **下載檔案**：使用 `get_url` 下載檔案，並儲存在指定路徑。

### 🧑‍💻 執行 Playbook

當你執行此 Playbook 時，服務會先啟動，目錄確保創建後，才會繼續下載檔案。這樣可以確保一切順利進行。

```bash
ansible-playbook -i inventory.ini nginx_setup.yml
```

---

### 🧠 提示

* 如果你依賴的是其他服務（例如 MariaDB、MySQL 等），可以依照同樣的邏輯操作，先啟動服務，再處理目錄和檔案。
* `wait_for` 模組在此處非常有用，它會確保服務在給定時間內成功啟動，這樣就不會出現目錄或檔案未創建的情況。

---

### 結語

這樣的 Playbook 會確保服務啟動後再進行後續操作。如果你的需求還有其他細節，或者需要更多的功能擴展，隨時告訴我，我可以幫你調整或添加更多的功能！😊
是的，當然！如果某個目錄是由某個服務（如 Nginx、MariaDB 等）創建的，那麼你必須先啟動並啟用該服務，等服務啟動後再進行對應的檔案操作或目錄處理。因此，這個任務確實應該往 **後面** 放，以確保目錄在該服務啟動並創建相關檔案後才出現。

---

### ✅ 設定順序

1. **啟動服務**：首先啟動服務，例如 Nginx 或 MariaDB，這樣相關目錄才能在服務啟動後正確生成。
2. **檢查並確保目錄存在**：接著確認目錄是否已經存在，如果不存在，可以使用 `file` 模組來創建目錄。
3. **執行後續任務**：完成後續的檔案下載、設定檔修改等任務。

---

### ✅ 範例 Playbook 設定

這是一個簡單的例子，展示如何先啟動服務，然後再處理目錄和檔案。

```yaml
- name: 安裝並設定 Nginx 和下載檔案
  hosts: all
  become: true
  tasks:

    # 安裝 Nginx
    - name: 安裝 Nginx
      dnf:
        name: nginx
        state: present
        update_cache: yes

    # 啟動並啟用 Nginx 服務
    - name: 啟動並啟用 Nginx 服務
      systemd:
        name: nginx
        state: started
        enabled: yes

    # 等待服務啟動並創建必要的目錄（例如 Nginx 的 web 根目錄）
    - name: 等待 Nginx 服務啟動
      wait_for:
        host: "{{ inventory_hostname }}"
        port: 80
        timeout: 60
        state: started

    # 確保目錄存在
    - name: 確保 Nginx 根目錄存在
      file:
        path: "/usr/share/nginx/html"
        state: directory
        mode: '0755'

    # 下載檔案（例如下載 Nginx 的預設網頁）
    - name: 下載預設網頁
      get_url:
        url: https://nginx.org/download/nginx-1.20.1.tar.gz
        dest: /tmp/nginx-1.20.1.tar.gz

    # 其他任務（如果有的話）
```

---

### 🔍 解釋

1. **安裝 Nginx**：使用 `dnf` 安裝 Nginx。
2. **啟動並啟用服務**：用 `systemd` 模組啟動並設置 Nginx 開機自啟動。
3. **等待服務啟動**：用 `wait_for` 等待 Nginx 在端口 80 上啟動，確保相關目錄被服務創建或初始化。
4. **確認目錄存在**：使用 `file` 模組確保目錄存在（這裡以 `/usr/share/nginx/html` 作為範例目錄）。
5. **下載檔案**：使用 `get_url` 下載檔案，並儲存在指定路徑。

### 🧑‍💻 執行 Playbook

當你執行此 Playbook 時，服務會先啟動，目錄確保創建後，才會繼續下載檔案。這樣可以確保一切順利進行。

```bash
ansible-playbook -i inventory.ini nginx_setup.yml
```

---

### 🧠 提示

* 如果你依賴的是其他服務（例如 MariaDB、MySQL 等），可以依照同樣的邏輯操作，先啟動服務，再處理目錄和檔案。
* `wait_for` 模組在此處非常有用，它會確保服務在給定時間內成功啟動，這樣就不會出現目錄或檔案未創建的情況。

---

### 結語

這樣的 Playbook 會確保服務啟動後再進行後續操作。如果你的需求還有其他細節，或者需要更多的功能擴展，隨時告訴我，我可以幫你調整或添加更多的功能！😊

# chatGPT 3 修改特定幾行內
在 Ansible 中，若你要修改特定幾行內容，或是建立新檔案並加入多行文字，可以使用幾個常用的模組來達成：

* **`lineinfile`**：用來修改檔案中的某一行。
* **`blockinfile`**：用來插入或更新多行文字到檔案的特定區塊。
* **`copy`**：用來創建新的檔案並寫入內容。
* **`template`**：使用 Jinja2 模板來創建並寫入檔案，適合需要更多動態內容的情境。

### 1. **修改檔案中的特定幾行：`lineinfile`**

如果你只是想修改檔案中的一行，`lineinfile` 模組非常適合。它可以確保檔案中某一行存在，並且能根據指定條件進行修改。

#### 範例：修改特定行

```yaml
- name: 修改特定行
  lineinfile:
    path: /etc/someconfig.conf
    regexp: '^# ExampleSetting'
    line: 'ExampleSetting=some_value'
    state: present
```

這段代碼會尋找檔案中的 `# ExampleSetting`，如果找到，就將其替換為 `ExampleSetting=some_value`。如果找不到，則會新增該行。

### 2. **建立新檔案並寫入多行文字：`copy`**

如果你想創建一個新檔案並寫入多行內容，可以使用 `copy` 模組，並利用 `content` 屬性來寫入多行文字。

#### 範例：創建新檔案並加入多行文字

```yaml
- name: 創建新檔案並寫入多行內容
  copy:
    dest: /etc/newfile.conf
    content: |
      # This is the first line
      setting1=value1
      setting2=value2
      # End of configuration
```

這段代碼會創建一個新的檔案 `/etc/newfile.conf`，並將以下多行文字寫入：

```
# This is the first line
setting1=value1
setting2=value2
# End of configuration
```

`|` 符號用來表示多行文字塊，每行會被保留。

### 3. **插入多行文字到檔案特定區塊：`blockinfile`**

如果你需要在檔案中插入多行文字到特定區塊（比如在某一段中插入設定），`blockinfile` 模組非常適合。

#### 範例：插入多行文字到檔案

```yaml
- name: 插入多行文字到檔案
  blockinfile:
    path: /etc/someconfig.conf
    block: |
      [newsection]
      setting1=value1
      setting2=value2
      setting3=value3
    marker: "# {mark} ANSIBLE MANAGED BLOCK"
```

這段代碼會將指定的多行文字區塊插入到 `/etc/someconfig.conf` 中。`marker` 用來標記插入區塊的範圍，這樣 Ansible 在以後的運行中會知道哪些區塊是管理過的。

---

### 4. **範例：修改指定行並創建新檔案的完整 Playbook**

如果你要在同一個 Playbook 中進行多項操作（例如修改某幾行並創建新檔案），可以這樣組合：

```yaml
---
- name: 修改檔案並創建新檔案
  hosts: all
  become: true
  tasks:
    # 修改特定行
    - name: 修改特定行
      lineinfile:
        path: /etc/someconfig.conf
        regexp: '^# ExampleSetting'
        line: 'ExampleSetting=some_value'
        state: present

    # 創建新檔案並寫入多行內容
    - name: 創建新檔案並寫入多行內容
      copy:
        dest: /etc/newfile.conf
        content: |
          # This is the first line
          setting1=value1
          setting2=value2
          # End of configuration

    # 插入多行文字到檔案特定區塊
    - name: 插入多行文字到檔案
      blockinfile:
        path: /etc/someconfig.conf
        block: |
          [newsection]
          setting1=value1
          setting2=value2
          setting3=value3
        marker: "# {mark} ANSIBLE MANAGED BLOCK"
```

---

### 🧠 **總結**

* **`lineinfile`**：用於修改單行或檢查某行是否存在，並根據條件進行更新。
* **`blockinfile`**：用於插入多行文字到檔案的特定區塊，適合於複雜的設定檔修改。
* **`copy`**：用來創建新檔案並寫入多行文字，適合從零開始創建檔案。

這些模組可以根據你的需求來選擇使用，讓你在管理設定檔時更方便。如果還有其他問題或需求，隨時告訴我！ 😊

# chatGPT 4

# chatGPT 5
