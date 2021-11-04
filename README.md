# unity-prefs-ex
Extension for player preferences based on Unity player preferences.

# install
Use this repo and import it directly in Unity.

# usage
Instead to use `PlayerPrefs` use class `PlayerPrefsEx` that supports additional types and functionality.

### Functionality
* Each write method supports a boolean value to store data immediately
* Each write method calls a `OnChanged` event (static event in `PlayerPrefsEx`)
* Each read method supports optional old keys for compatibility. Setup old keys here to transfer values to new key
* Support to search multiple keys with one call
* Support to delete multiple keys with one call

### Types
* Boolean
* Integer
* Long
* Float
* Double
* String
* Character
* Byte Array
* Date Time
* Time Span
* Enum

