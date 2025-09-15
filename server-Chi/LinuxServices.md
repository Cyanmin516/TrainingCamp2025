Linux Servies
1. IP
2. FTP
3. DNS
4. 套件管理
5. Database
6. web server
7. programing-php
8. phpmyadmin
9. wordpress

## 1. sample of IP
```sh
ifconfig
ip addr add {IP} dev {ens}
ip route show
ip route add default via {IP} dev {ens}
```

## 2


## 3. sample of DNS
```sh
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
```sh
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
```sh
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
```sh
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
```sh
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
```sh
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
```sh
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
