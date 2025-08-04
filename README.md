# ScarletTrash

**ScarletTrash** is a V Rising mod that automatically deletes items placed in containers with "STrash" in their name.

---

## Support & Donations

<a href="https://www.patreon.com/bePatron?u=30093731" data-patreon-widget-type="become-patron-button"><img height='36' style='border:0px;height:36px;' src='https://i.imgur.com/o12xEqi.png' alt='Become a Patron' /></a>  <a href='https://ko-fi.com/F2F21EWEM7' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' alt='Buy Me a Coffee at ko-fi.com' /></a>

---

## How It Works

Rename any container to include "STrash" as a separate word, then place unwanted items inside. The mod will wait 15 seconds before automatically and permanently deleting all items, giving you a chance to recover them if needed.

When you rename a container to include "STrash", you'll receive a warning message confirming the container is now a trash bin and reminding you that items will be permanently deleted.

**Notes:**
* The name check is case-sensitive and must be a separate word for security - only "STrash" (exact capitalization) will work, not "strash", "STRASH", "Strash", "ItemSTrash", or "STrashItems".
* The 15-second timer resets every time you add, remove, or move items within the container. Items are only deleted after 15 seconds of no inventory changes.

**Warning:** Items are permanently deleted and cannot be recovered after the 15-second delay!

## Installation

### Requirements

This mod requires the following dependencies:

* **[BepInEx](https://wiki.vrisingmods.com/user/bepinex_install.html)**
* **[ScarletCore](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletCore/)**

Make sure BepInEx is installed and loaded **before** installing ScarletTrash.

### Manual Installation

1. Download the latest release of **ScarletTrash**.

2. Extract the contents into your `BepInEx/plugins` folder:

   `<V Rising Server Directory>/BepInEx/plugins/`

   Your folder should now include:

   `BepInEx/plugins/ScarletTrash.dll`

3. Ensure **ScarletCore** is also installed in the `plugins` folder.
4. Start or restart your server.


## This project is made possible by the contributions of the following individuals:

- **cheesasaurus, EduardoG, Helskog, Mitch, SirSaia, Odjit** & the [V Rising Mod Community on Discord](https://vrisingmods.com/discord)
