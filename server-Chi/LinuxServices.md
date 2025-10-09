Linux Servies
0. root
1. IP
2. FTP
3. DNS
4. 套件管理
5. Database
6. web server
7. programing-php
8. phpmyadmin
9. wordpress
<<<<<<< HEAD
10. sFTP
11. firewall

## 0. root
```bash
sudo adduser new_user_name
For Ubuntu/Debian:
sudo usermod -aG sudo new_user_name
For Red Hat/CentOS/Fedora:
sudo usermod -aG wheel new_user_name
sudo visudo
###
root    ALL=(ALL:ALL) ALL
new_user_name  ALL=(ALL:ALL) ALL
###
sudo passwd root
```
=======
10. mail
>>>>>>> 8c0494c (linux postfix and dovecot)

## 1. sample of IP
```bash
ifconfig
ip addr add {IP} dev {ens}
ip route show
ip route add default via {IP} dev {ens}
```

## 2 FTP
```sh
root# dnf install vsftpd

停止抓取extras-common
grep extras-common /etc/yum.repo.d/*
vim /etc/yum.repo.d/centos-addons.repo
###IN vim
[extras-common]
enable=0
###

多安裝幾次，拿到全部安裝
vim /etc/vsftpd/vsftpd.conf
###IN vim
啟動下列服務，刪除#註解
xferlog_file=/var/log/xferlog
idle_session_timeout=600
data_connection_timeout=120
###

systemctl enable --now vsftpd
sftp user@localhost
dnf ftp
ftp IP
###IN ftp
[yes/footprint]
ftp> anomyous
password: [email@formate]
ls
exit
###

設定防火牆並永遠允許
firewall-cmd --add-service=ftp
firewall-cmd --add-service=fip --permanent

UPLOAD
ftp> put file-path server-path
DOWNLOAD
ftp> get file-path your-path
```

ftp : 21-port 控制, 20-port 資料傳輸



## 3. sample of DNS
```bash
#vim /etc/named.conf
listen-on port 53 {`ip;` 127.0.0.1;};
註解IPv6, `//listen-on-v6`
allow-query {`IP.0/24;` localhost;}; #一個網段
incldue "/etc/named/student.conf"; #las line
:wq

#vim /etc/named/student.conf
zone "studentX.example.com" IN{
    type master;
    file "/var/named/student.zone";
};

#vim /var/named/student.zone 
$TTL 10
@   IN SOA dns.studentX.example.com. root ( ;註解用分號 @為本地 dns最後要加.
    2025001 ;serial number
    1H 
    2D ;try again backoff
    3W ;try until
    10 ;life time
)

@   IN NS dns.studentX.com.


systemctl enable --now named
firewall-cmd --add-service=dns
firewall-cmd --add-service=dns --permanent

#vim /etc/resolv.conf
search student103.example.com
nameserver 10.10.53.103
nameserver 10.10.2.8

dig www.student.com.tw
```


## 4. sample of 套件
```bash
cd /etc/yum.repos.d/
vim centos.repo
### IN VIM
[baseos]
必須註解原有連結
#metalink=https://mirrors.centos.org/metalink?repo=centos-baseos-$stream&arch=$basearch&protocol=https,http
設定新站台
baseurl=https://mirror.twds.com.tw/centos-stream/9-stream/BaseOS/x86_64/os/ #設定新站台

[appstream]
必須註解原有連結
#metalink=https://mirrors.centos.org/metalink?repo=centos-appstream-$stream&arch=$basearch&protocol=https,http
設定新站台
baseurl=https://mirror.twds.com.tw/centos-stream/9-stream/AppStream/x86_64/os/ 
:wq
### END VIM

vim epel.repo
### IN VIM  
[epel]
必須註解原有連結
#metalink=https://mirrors.fedoraproject.org/metalink?repo=epel-9&arch=$basearch&infra=$infra&content=$contentdir 
設定新站台
baseurl=https://mirror.twds.com.tw/fedora/epel/9/Everything/x86_64/ 
:wq
### END VIM

vim epel-cisco-openh264.repo
### IN VIM
[epel-cisco-openh264]
改為不啟用
enable=0
### END VIM

dnf clean all
yum clean all
#redo install
dnf -y install bind bind-chroot bind-utils
```


## 5 database
mariadb
```bash
dnf install mariadb mariadb-server
systemctl enable --now mariadb
mysql_secure_installation
mysql -u root -p -h localhost

firewall-cmd --add-service=mysql
firewall-cmd --add-service=mysql --permanent
systemctl enable --now mariadb
```

## 6 web server
HTTP server
```bash
dnf module list nginx
dnf module enable nginx:1.26
dnf module install nginx
systemctl enable --now nginx
firewall-cmd --add-service=http
firewall-cmd --add-service=http --permanent
systemctl enable --now nginx
```


## 7 Programing
 php
```bash
dnf module list php
指定版本
dnf module enable php:8.3
dnf install php php-fpm
vim /usr/share/nginx/html/index.php
system enable --now php-fpm
system restart nginx
```

```php
<?php
phpinfo();
>
```


## 8 phpmyadmin
phpmyadmin
management mysql and mariadb
```bash
解壓縮
mv /home/manger/下載/phpMyAdmin-5.2.2-all-languages.tar.xz  /usr/share/nginx/html/
cd /usr/share/nginx/html
tar -Jxvf phpMyAdmin-5.2.2-all-languages.tar.xz 

mv phpMyAdmin-5.2.2-all-languages phpMyAdmin
dnf install php-mysqlnd php-pdo
cp phpMyAdmin/config.sample.inc.php phpMyAdmin/config.inc.php
vim phpMyAdmin/config.inc.php
###
$cfg['blowfish_secret'] = 'any string u can write in'; /* YOU MUST FILL IN THIS FOR COOKIE AUTH! */
###
重啟
systemctl restart php-fpm
systemctl restart nginx
虛擬目錄
restorecon -Rvv phpMyAdmin
```



## 9 wordpress
```bash
dnf module list mariadb
systemctl stop mariadb
dnf remove mariadb-*
dnf module enable mariadb:10.11
dnf module install mariadb
systemctl enable --now mariadb
mysql_secure_install
mv /home/manager/下載wordpress-6.8.2-zh_TW.zip /usr/share/nginx/html/
cd /usr/share/nginx/html/
unzip wordpress-6.8.2-zh_TW.zip
cd wordpress
mv wp-config-sample.php wp-config.php
vim wp-config.php
###
define('DB_NAME','wordpress' );
define('DB_USER','wordpress' );
define('DB_PASSWORD','WordPress@website103' );
###
restorecon -Rvv .
```

phpMyAdmin 登入
新增 權限 user 
新增 資料庫 wordpress / utf8-general-ci

<<<<<<< HEAD


## 10. FTP
1. 查詢軟體 : vftp
2. 安裝軟體
3. 服務設定 : 
4. 啟動服務 : systemctl enable --now
6. 防火牆設定 : firewall-cmd
5. 測試 : 

```bash
root# dnf install vsftpd

停止抓取extras-common
grep extras-common /etc/yum.repo.d/*
vim /etc/yum.repo.d/centos-addons.repo
###
[extras-common]
enable=0
###

多安裝幾次，拿到全部安裝
vim /etc/vsftpd/vsftpd.conf
###
啟動下列服務，刪除#註解
xferlog_file=/var/log/xferlog
idle_session_timeout=600
data_connection_timeout=120
###

systemctl enable --now vsftpd

sftp user@localhost
dnf ftp
ftp IP
###
[yes/footprint]
ftp> anomyous
password: [email@formate]
ls
exit
###

firewall-cmd --add-service=ftp
firewall-cmd --add-service=fip --permanent

UPLOAD
ftp> put file-path server-path
DOWNLOAD
ftp> get file-path your-path
```


## 11. Firewall
`firewalld` 是一個動態防火牆管理工具，它將常見的網路服務和它們使用的通訊埠（port）預先定義為「服務」。這樣做的好處是，使用者不需要記憶每一個服務所對應的通訊埠號碼。

當你執行 `firewall-cmd --add-service=smtp` 時，`firewalld` 會在它的設定中查找 `smtp` 這個服務。`firewalld` 的服務定義檔通常位於 `/usr/lib/firewalld/services/` 或 `/etc/firewalld/services/` 目錄。

你可以查看 `smtp.xml` 這個檔案的內容來驗證：

```bash
cat /usr/lib/firewalld/services/smtp.xml
```

這個檔案的內容會類似這樣：

```xml
<?xml version="1.0" encoding="utf-8"?>
<service>
  <short>SMTP</short>
  <description>SMTP is used for sending e-mails between two hosts.</description>
  <port protocol="tcp" port="25"/>
</service>
```

從這個 XML 檔案可以看出，`smtp` 服務被定義為使用 **TCP 協定**的**通訊埠 25**。

-----

### Postfix 與 `firewalld` 的關係

`postfix` 是一個 MTA（Mail Transfer Agent），它負責處理郵件的發送和接收。當你安裝並啟動 `postfix` 後，它會預設在通訊埠 **25** 上監聽，等待接收和傳送郵件。

所以，當你執行 `firewall-cmd --add-service=smtp` 時，實際上是告訴防火牆：

1.  找到名為 `smtp` 的服務定義。
2.  發現 `smtp` 服務對應的是 **TCP 通訊埠 25**。
3.  在防火牆規則中，**開放所有進入通訊埠 25 的 TCP 連線**。

這就允許外部的郵件伺服器能夠連線到你主機的通訊埠 25，與 `postfix` 進行溝通，從而發送或接收郵件。

總結來說，`postfix` 和 `firewall-cmd` 是**兩個獨立的元件**，它們之間沒有直接的「認知」關係。`postfix` 只是碰巧使用 `firewalld` 已經預先定義好的 `smtp` 服務所對應的通訊埠。`firewalld` 是一個通用的工具，它定義了許多常見的服務，讓管理員可以透過服務名稱來操作防火牆，而無需記住每個服務的具體通訊埠號碼。
=======
## 10 mail
情境
user1-mailserver1-mailserver2-user2
協定
user1-`SMTP`-`SMTP`-mailserver2-`POP3/IMAP`-user2

user2-`SMTP`-`SMTP`-mailserver1-`POP3/IMAP`-user1

```bash
root#
搜尋安裝
dnf search postfix
dnf install postfix
設定
vim /etc/postfix/main.cf
#in vim
hostname= mail.studnet103.example.com
mydomain= student103.example.com
myorigin= $mydomain
inet_interfaces=all
inet_protocols =all
mydestination = $myhostname, localhost.$mydomain, localhost, $mydomain
mynetworks _style = class
mynetworks = 10.10.53.0/24, 127.0.0.0/8
home_mailbox = Maildir/
#end vim
第一次啟動
system enable --now postfix
更新設定後重啟
system restart postfix
system stop postfix
system start postfix
防火牆
firewall-cmd --add-service=smtp
firewall-cmd --add-service=smtp --permanent
測試
telnet 127.0.0.1 25
設定網域
vim /var/named/student.zone

#in vim
新增
@ IN MX 10 mail.student103.example.com.
郵件轉送MX優先度10透過網站

設定DNS網站對應IP
mail.student30.example.com. IN A 10.10.53.103
#end vim

mail -s "test" manger@mail.student103.example.com
#IN mail
your messages
ctrl+D to quit
#
mailq
cat /var/log/maillog

su manger
cd Maildir/
cd new
ls
cat mail-ID
```

### 收信箱
dovecot
```bash
dnf install dovecot

vim /etc/dovecot/dovecot.conf
#IN VIM
listen=: *, ::
#

vim /etc/dovecot/conf.d/10-auth.conf
#IN VIM
line 10 : diable_plaintext_auth = no
line 100: auth_mechanisms = plain login
#

vim /etc/dovecot/conf.d/10-mail.conf
#IN VIM
mail_location = maildir:~/Maildir
#

vim /etc/dovecot/conf.d/10-master.conf
#in vim
line 107: 
    #postfix smtp-auth
unix_listener /var/spool/postfix/private/auth {
    mode = 0666
    user = postfix
    group = postfix
}
#

vim /etc/dovecot/conf.d/10-ssl.conf
#IN VIM
ssl = yes
#

firewall-cmd --add-service={imap,pop3,imaps,pop3s}
firewall-cmd --add-service={imap,pop3,imaps,pop3s} --permanent

systemctl enable --now dovecot
```











>>>>>>> 8c0494c (linux postfix and dovecot)
