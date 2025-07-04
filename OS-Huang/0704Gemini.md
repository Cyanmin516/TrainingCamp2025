# Windows Server 2022 Domain Controller Services

**Speaker**: Professor Huang
**Date**: July 4, 2025
**Topic**: Domain Controller Management

## Prerequisites

1.  **Virtual Machines (VMs)**: We're working with three VMs:
      * File Server
      * PC1
      * Domain Controller (DC)
2.  **Course Completion**: Before this session, ensure you've covered:
      * Network Configuration
      * NTFS Permissions
      * Shared Network Drives

-----

## Domain Password Policy

To configure the domain password policy:

1.  Navigate to **Tools** \> **Group Policy Management**.
2.  Expand the **Forest** and then your **Domain**.
3.  Right-click on **Default Domain Policy** and select **Edit**.
4.  Within the Group Policy Management Editor, go to:
      * **Computer Configuration**
      * **Policies**
      * **Windows Settings**
      * **Security Settings**
      * **Account Policies**
      * **Password Policy**

### Force Policy Update

To immediately apply these policy changes across the domain, run the following command on the DC:

```bash
gpupdate /force
```

-----

## User Management

### Active Directory Domain Controller (AD DC)

Manage users and computers within your domain:

1.  Open **Active Directory Users and Computers**.
2.  Expand your domain (e.g., `lab03.com.tw`).
3.  The **Computers** container lists all joined domain computers.

### DNS Management

DNS (Domain Name System) and DHCP (Dynamic Host Configuration Protocol) are distinct network services with different functions and management scopes.

1.  Open **DNS Manager**.
2.  Navigate to your **DC**.
3.  Expand **Forward Lookup Zones**.
4.  Select your domain (e.g., `lab03.com.tw`). You'll see DNS records for your domain.

-----

## Joining the Domain

### File Server

To add your File Server to the domain:

1.  On the File Server VM, modify its **DNS settings** to point to your Domain Controller.
2.  Change the **Workgroup** to **Domain**.
3.  Log in with the **Domain Administrator** credentials (e.g., `administrator/your_password`).
4.  After the "Welcome to the Domain" message, **restart** the File Server.
5.  On the DC's DNS, verify that the File Server is registered as an **A record (IPv4 Host)**.

### General Computer (e.g., PC1)

To add a general client PC to the domain:

1.  Right-click the **Windows Start button** and select **System**.
2.  Click **Rename this PC (Advanced)**.
3.  Change the **Domain** and provide the domain administrator credentials.
      * If successful, you'll be prompted to **restart**. Skip to step 6.
4.  If direct domain join fails, use the **Network ID Wizard**:
      * Select **This computer is part of a business network**.
      * Choose **My company uses a network with a domain**.
      * Attempt to log in (may fail initially).
      * Select **Add the following domain user**.
      * Choose **Standard account**.
5.  **Restart** the computer.
6.  Log in using the format: `domain_name\domain_user` and the corresponding `password` (e.g., `lab03.com.tw\username`).

-----

### Adding Users

  * While you can create local users on the File Server, its password policy will be **overridden and locked** by the DC's domain policy.
  * **File servers typically do not manage local users.** User management should be centralized through Active Directory on the DC.
  * Each domain login creates a **user profile directory** on the local machine.

-----

## File Server Sharing

  * Add the **`Domain Users`** group to the **NTFS permissions** of your shared folders to allow domain-authenticated users access.
  * Grant **Full Control** for sharing permissions.
  * **Disable the default `Users` group permissions** to prevent local users from bypassing DC control. By default, NTFS allows all users read access.

### Reviewing Sharing Configuration

1.  On your File Server, navigate to the **D drive**.

2.  Right-click on the folder you want to share.

      * **NTFS Permissions**:
          * Go to **Grant access to** \> **Specific people**.
          * Or, go to **Properties** \> **Security** \> **Advanced**.
      * **Sharing Permissions**:
          * Go to **Sharing** \> Click **Share**.
          * In **Advanced Sharing**, click **Permissions** \> **Add** \> **Advanced** \> **Find Now** to locate existing users or groups (e.g., `Domain Users`). Set **Full Control** here.
          * Sharing settings are typically configured only at the **top-level folder** and inherit down. `Domain Users` ensures only authenticated domain accounts can access.

3.  Ensure **Network Discovery** is enabled on the client PC (e.g., PC1).

4.  On the client PC, open **File Explorer**.

5.  Right-click **This PC** \> **Map network drive**.

      * Choose a **drive letter**.
      * Enter the network path: `\\fileserver\share` or `\\IP_address\share_name`.

-----

### User Recreation

If a user is accidentally deleted:

  * Their **profile data and permissions will be removed**.
  * You **cannot directly restore** an account with the same username and password.
  * You will need to **re-create the account**, add it back to necessary groups, and reconfigure any specific permissions.

### User Visibility (Access-Based Enumeration)

To hide folders from users who don't have read access on the `fileserver`:

1.  Open **Server Manager**.
2.  Go to **File and Storage Services**.
3.  Select **Shares**.
4.  Right-click on the shared folder and select **Properties**.
5.  Go to **Settings**.
6.  Enable **Access-Based Enumeration**.

-----

### Terminology Changes:

  * `pro.Huang` -\> `Professor Huang`
  * `DS` -\> `Domain Controller (DC)`
  * `共享網路磁碟機` -\> `Shared Network Drives`
  * `Default Domain Policy` -\> `Default Domain Policy` (no change, just formatting)
  * `網域` (under Password Policy) -\> `Domain`
  * `電腦設定` -\> `Computer Configuration`
  * `原則` -\> `Policies`
  * `Windows設定` -\> `Windows Settings`
  * `安全性原則` -\> `Security Settings`
  * `帳戶原則` -\> `Account Policies`
  * `密碼原則` -\> `Password Policy`
  * `強制網域內立即更新` -\> `Force Policy Update`
  * `Active Directory 使用者和電腦` -\> `Active Directory Users and Computers`
  * `網域內電腦` -\> `joined domain computers`
  * `主機(A)` -\> `A record (IPv4 Host)`
  * `登入 administrator/password 這是DC使用者` -\> `Log in with the Domain Administrator credentials (e.g., administrator/your_password)`
  * `win右鍵` -\> `Right-click the Windows Start button`
  * `重新命名此電腦(進階)` -\> `Rename this PC (Advanced)`
  * `網路識別` -\> `Network ID Wizard`
  * `選商業網路` -\> `Select This computer is part of a business network`
  * `選有網域的網路` -\> `Choose My company uses a network with a domain`
  * `選新增下列網域使用者` -\> `Select Add the following domain user`
  * `選標準帳戶` -\> `Choose Standard account`
  * `網域名稱\網域使用者` -\> `domain_name\domain_user`
  * `本機` (when referring to administrator account) -\> `local`
  * `lab30.com.tw\administrator` -\> `lab03.com.tw\administrator`
  * `labmen` -\> `domain-authenticated users`
  * `共用權限全部打勾` -\> `Grant Full Control for sharing permissions`
  * `停用users避免本機使用者繞過DS控制權限` -\> `Disable the default Users group permissions to prevent local users from bypassing DC control`
  * `伺服器管理員` -\> `Server Manager`
  * `檔案和存放服務` -\> `File and Storage Services`
  * `共用` (under Server Manager) -\> `Shares`
  * `設定` (under Shares) -\> `Settings`
  * `啟用存取型列舉` -\> `Access-Based Enumeration`


