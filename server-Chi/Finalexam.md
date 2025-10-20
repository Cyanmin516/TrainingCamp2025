建置Linux服務快速版本
last edit day1020 time1611

善用記事本>編輯>取代>全部取代
Linux Ctrl+shirt+V貼上
改前另存新檔test.txt，可以剪下指令操作

IP(要改)===========
192.168.140.100
Network(要改)======
192.168.140.0/24
Domain(要改)=======
passwordleak.com

下面設定設mariadb    帳號root密碼(要改)Linuxroot
下面設定設phpMyAdmin 帳號root密碼(要改)rootdbadmin
下面設定設wordpress  帳號(要改)wordpressu密碼(要改)wordpressp


#1. 設定與下載確認下載完畢
su -
Linuxroot
dnf install bind bind-chroot bind-utils mariadb mariadb-server php-mysqlnd php-pdo php php php-fpm nginx vsftpd -y 

#確認全部安裝
#確認全部安裝
#確認全部安裝，重複步驟一

#防火牆
firewall-cmd --add-service={dns,http,mysql}
firewall-cmd --add-service={dns,http,mysql} --permanent


#2. DNS 

vim /etc/named.conf
```
#更改部分參數，右下角 11,9代表行列，看左邊的數字
#修改第11行 IP
listen-on port 53 { 192.168.140.100; 127.0.0.1; };
#修改第19行 Network
allow-query     { 192.168.140.0/24/24; localhost; };
#新增第59行
include "/etc/named/student.conf";
```

vim /etc/named/student.conf
```
zone "passwordleak.com" IN{
    type master;
    file "/var/named/student.zone";
};
```

vim /var/named/student.zone
```
$TTL 10
@ IN SOA dns.passwordleak.com. root (
         2025102001 ;serial YYYYMMDDnn
         1H
         2D
         3W
         10
)
 
@ IN NS dns.passwordleak.com.
dns IN A 192.168.140.100
www IN A 192.168.140.100
dbadmin IN A 192.168.140.100
```

vim /etc/resolv.conf
```
#最前面插入
search passwordleak.com
nameserver 192.168.140.100
```
systemctl enable --now named

#3. Database

systemctl enable --now mariadb

#4. nginx and php
systemctl enable --now php-fpm
systemctl enable --now nginx

vim /usr/share/nginx/html/index.php
```php
<?php
phpinfo();
?>
```
systemctl restart nginx

#5. phpMyAdmin
cd /usr/share/nginx/html
wget https://files.phpmyadmin.net/phpMyAdmin/5.2.2/phpMyAdmin-5.2.2-all-languages.zip /usr/share/nginx/html
unzip phpMyAdmin-5.2.2-all-languages.zip 
mv phpMyAdmin-5.2.2-all-languages phpMyAdmin
cp phpMyAdmin/config.sample.inc.php  phpMyAdmin/config.inc.php

vim phpMyAdmin/config.inc.php 
```
#更改部分參數
#新增簽證32-位元(可以看右下角 第16列,第60字)
$cfg['blowfish_secret'] = 'asdfzxcvasdfqwerjjkl;lkajdsfoi45';
```

systemctl restart php-fpm
systemctl restart nginx

mysql_secure_installation
```
Linuxroot
y
y
rootdbadmin
rootdbadmin
y
y
y
y
```

#測試
mysql -u root -p -h localhost
rootdbadmin
MariaDB> exit

systemctl restart mariadb

#6. wordpress
cd /usr/share/nginx/html
wget https://tw.wordpress.org/latest-zh_TW.zip /usr/share/nginx/html
unzip /usr/share/nginx/html/latest-zh_TW.zip
mv wordpress/wp-config-sample.php wordpress/wp-config.php


vim wordpress/wp-config.php
```
#更改部分參數
#修改第23行
define( 'DB_NAME', 'wordpress' );
#修改第26行
define( 'DB_USER', 'wordpressu' );
#修改第29行
define( 'DB_PASSWORD', 'wordpressp' );
```

vim /etc/nginx/conf.d/wp.conf
```
server {
    listen 80;
    server_name www.passwordleak.com;
    root /usr/share/nginx/html/wordpress;

    error_log /var/log/nginx/wp_error.log;
    include /etc/nginx/default.d/*.conf;
}
```

vim /etc/nginx/conf.d/dbadmin.conf
```
server {
    listen 80;
    server_name dbadmin.passwordleak.com;
    root /usr/share/nginx/html/phpMyAdmin;

    error_log /var/log/nginx/db_error.log;
    include /etc/nginx/default.d/*.conf;
}
```
#重啟設定
nginx -t
nginx -s reload


#網頁phpMyAdmin 登入
http://dbadmin.passwordleak.com/
使用者root
密碼rootdbadmin
#登入

http://dbadmin.passwordleak.com/index.php?route=/server/databases
#資料庫
建立資料庫 wordpress / utf8-general-ci 建立
#或者
建立資料庫 wordpress / utf8mb3-general-ci 建立

確認資料庫名稱與編碼後，點擊建立


http://dbadmin.passwordleak.com/index.php?route=/server/privileges&db=wordpress&checkprivsdb=wordpress&viewing_mode=db
#(資料庫wordpress ) 權限 
新增 新增使用者

#依據wp-config.php設定
使用者名稱= wordpressu
主機名稱= 本機   localhost
密碼= wordpressp
重新輸入= wordpressp

全域權限<打勾>

**執行**，在最底部

#成功
#已新增了新的使用者。 

############設定結束

#phpMyAdmin網址
dbadmin.passwordleak.com

#Wordpress網址
www.passwordleak.com



0. ROOT權限
sudo passwd
Linuxroot

su -
Linuxroot

vim ~/.vimrc
```
set nu
set statusline=%F
```
dnf list installed | egrep 'bind|mariadb|php|nginx'
ip address 
ping 8.8.8.8

0.1.2
測試
dig www.passwordleak.com
得到
;; ANSWER SECTION:
www.passwordleak.com.	10	IN	A	192.168.140.100

Windows
C:\Users\user>nslookup www.passwordleak.com
伺服器:  UnKnown
Address:  192.168.140.100

名稱:    www.passwordleak.com
Address:  192.168.140.100

0.1.5
測試
dbadmin.passwordleak.com
登入後可以新增使用者


0.6
網頁wordpress
www.passwordleak.com
設定下列資料
標題 student
使用者名稱 student07
密碼 std@dmin
V確認弱密碼
Email student@passwordleak.com
