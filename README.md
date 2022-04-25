# CippSharpCoreAttributes
Attributes and relative 'decorators' and 'drawers'.
These are attributes intended to have CustomPropertyDrawers for Inspector.

### Purpose: 
A lighter solution than [Odin](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) _(cough cough)_.
These attributes are more unity-oriented without re-serializing 
everything too much.

Decorators are the best choice if you want to add custom drawers 
to a property without affecting default inspector

### Contents:
There are different attributes for different situations.
Please read each attribute comment / see the examples.
Some of these are also Decorators for a better customization experience.
- NotEditable → draws a not editable field.
- NotEditableInPlay → draws a not editable field in play. This is editable during edit mode.
- ShowIf → draws an editable field if the condition is matched, otherwise the field is hidden or NotEditable.
- BoolButton → draws a bool field in a button way.
- BoolButtonCallback → draws a bool field in a button way and you may attach a callback to it.
- HelBox series → draws an HelpBox on a field.
- Preview → preview attribute! It previews the Object field if it is possible.
- PreviewCustomTexture → previews a custom texture.
- SceneSelector → you can select which scene from a string field
- TagSelector → you can select which tag from string field
- ClampMinMaxValue → clamp a min and a max of a float or an int field.
- BitMask → draws a flag enum using MaskField of Unity
- EnumMask → draws a flag enum using a raw but sometimes more accurate way to represent it.
- DisplayName → overrides field display name
- DisplayNameSuffix → add a suffix to field display name
- Assert → (I don't know when and why I created this attribute, but... see the examples)
- MinMax → Contributors (one friend of mine). Have a look.

### How to Install:
- Option 1 (readonly) now it supports Unity Package Manager so you can download by copy/paste the git url in 'Package Manager Window + Install From Git'.
  As said this is a readonly solution so you cannot access all files this way.
- Option 2 (classic) download this repository as .zip; Extract the files; Drag 'n' drop the extracted folder in your unity project (where you prefer).
- Option 3 (alternative) add this as submodule / separate repo in your project by copy/paste the git url
  
### Code:
- use your preferred attributes at will!

### History:
I was always charmed by the possibility of customization in UnityEditor with your scripts, Custom
Editors and drawers. I tried different plugins I also bought [Odin](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)
but... they give a too and massive serialization to what I really need.
KISS principle _keep it simple stupid_. So I decided and created my own library of attributes and decorators.
If I need a customization or something I do it my way. 

### Links:
- [repo](https://github.com/ZiosTheCloudburster/CippSharpCoreAttributes.git)

### Support:
- [tip jar](https://www.amazon.it/photos/share/Gbg3FN0k6pjG6F5Ln3dqQEmwO0u4nSkNIButm3EGtit)
 
