# Final exam
建立虛擬機
完成服務

DNS, web, database

系統: linux centOS
模擬器: VMware workstation pro 17
網路設定: bridge

指令

## 0 環境設定
- 網路
    - 橋接網路 : 手動設定ip/gateway/dns
    - NAT網路  : ip address 記下網路IP

- 設定root
```
sudo passwd root
[rootpassword]
su -
[rootpassword]
```
- 設定vim
```
vim ~/.vimrc
set number
set statusline=%F
```


## DNS

dnf install bind bind-chroot bind-utils

vim /etc/named.conf

listen-on port 53 { `192.168.58.1;` 127.0.0.1; };

allow-query     { `192.168.58.0;` localhost; };

`include "/etc/named/student.conf";`

vim /etc/named/student.conf
```
zone "studentXX.example.com" IN{
    type master;
    file "/var/named/student.zone";
};
```

vim /var/named/student.zone
```
 $TTL 10
 @ IN SOA dns.studentXX.example.com. root(
         2025100101 ;serial YYYYMMDDnn
         1H
         2D
         3W
         10
 )
 
 @ IN NS dns.studentXX.example.com
 
 dns IN A 192.168.58.1
 www IN A 192.168.58.1
 dbadmin IN A 192.168.58.1
```

vim /etc/resolv.conf
```
最前面插入
search studentXX.example.com
nameserver 192.168.58.1
```

firewall-cmd --add-service=dns 

firewall-cmd --add-service=dns --permanent

systemctl enable --now named

測試

dig www.studentXX.example.com

得到

;; ANSWER SECTION:

www.studentXX.example.com.	10	IN	A	192.168.58.1


## Mariadb

dnf module enable mariadb:10.11 -y

dnf install mariadb mariadb-server

firewall-cmd --add-service=mysql 

firewall-cmd --add-service=mysql --permanent

systemctl enable --now mariadb 

mysql_secure_installation

[Rootpassword]
`y`
`y`

[mysqlPassword]

[mysqlPassword]
`y`
`y`
`y`
`y`

測試

mysql -u root -p -h localhost

MariaDB> exit


## nginx

dnf module enable nginx:1.26 -y

dnf install nginx -y

systemctl enable --now nginx

firewall-cmd --add-service=http

firewall-cmd --add-service=http --permanent

## php

dnf module enable php:8.3 -y

dnf install php php-fpm -y

vim /usr/share/nginx/html/index.php
```php
<?php
phpinfo();
?>
```
systemctl enable --now php-fpm

systemctl restart nginx


## phpMyAdmin

下載

google搜尋phpmyadmin

download from website and manual set 5.2.2

https://www.phpmyadmin.net/files/5.2.2/

dnf install php-mysqlnd php-pdo

移動與重新命名

mv /home/username/下載/phpMyAdmin-5.2.2-all-languages.zip  /usr/share/nginx/html/

cd /usr/share/nginx/html/

unzip phpMyAdmin-5.2.2-all-languages.zip 

mv phpMyAdmin-5.2.2-all-languages phpMyAdmin

cp phpMyAdmin/config.sample.inc.php  phpMyAdmin/config.inc.php

vim phpMyAdmin/config.inc.php 
```
新增簽證32-位元(可以看右下角 第16列,第60字)
$cfg['blowfish_secret'] = 'asdfzxcvasdfqwerjjkl;lkajdsfoi45';
```
systemctl restart php-fpm

systemctl restart nginx
 
測試

www.studentXX.example.com/phpMyAdmin

登入後可以新增使用者
 
## wordpress
google搜尋wordpress 下載

下載6.8.3

mv /home/a/下載/wordpress-6.8.3-zh_TW.zip /usr/share/nginx/html/

unzip /usr/share/nginx/html/wordpress-6.8.3-zh_TW.zip 

cd /usr/share/nginx/html/wordpress

cp wp-config-sample.php wp-config.php

vim wordpress/wp-config.php
```
define( 'DB_NAME', 'wordpress' );
define( 'DB_USER', 'wordpress' );
define( 'DB_PASSWORD', 'Wordpassword' );
```

vim /etc/nginx/conf.d/wp.conf
```
server {
    listen 80;
    server_name www.studentXX.example.com;
    root /usr/share/nginx/html/wordpress;

    error_log /var/log/nginx/wp_error.log;
    include /etc/nginx/default.d/*.conf;
}
```

vim /etc/nginx/conf.d/dbadmin.conf
```
server {
    listen 80;
    server_name dbadmin.studentXX.example.com;
    root /usr/share/nginx/html/phpMyAdmin;

    error_log /var/log/nginx/db_error.log;
    include /etc/nginx/default.d/*.conf;
}
```
重啟設定
nginx -t
nginx -s reload


**phpMyAdmin 登入**
dbadmin.studentXX.example.com

登入

新增 資料庫 wordpress / utf8-general-ci

(wordpress database)新增 權限 新增使用者

依據wp-config.php設定

使用者=username

密碼=userpassword

資料庫=本機localhost

執行，在最底部

**wordpress**
www.studentXX.example.com

設定下列資料

標題 `Student`

使用者名稱 `wordpress`

密碼 `Wordpassword`

V確認弱密碼

Email `student@studentXX.example.com`



### 指定版本
```bash
dnf module enable mariadb:10.11 -y
dnf install mariadb
dnf install php-mysqlnd php-pdo
dnf module enable nginx:1.26 -y
dnf install nginx
dnf module enable php:8.3 -y
dnf install php php-fpm
```