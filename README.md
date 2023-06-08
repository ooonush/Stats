# Stats

Most games include a stat system, whether it's a simple health scale with a fixed value or a more complex RPG system with the ability to pump and many interconnected stats that are calculated over the course of the game.
The purpose of the **Stats** assets is to make it easier for Unity developers to add, maintain, and extend this kind of functionality to their games.

_This version is in **preview** stage and may be unstable, it is **not recommended to use it in production**. If you encounter any bugs, please make a bug report. You can also suggest features that you would like to see in the Asset._

## Multiplayer Support

**Stats** has support for multiplayer using **FishNet**. For this you need to install the **[FishyStats](https://github.com/ooonush/FishyStats)** addon.

## Installation

Requires a version of **Unity 2022.3** or newer. You can add `https://github.com/ooonush/Stats.git?path=Assets/Plugins/Stats` to **Package Manager**

![image](https://github.com/ooonush/Stats/assets/72870405/40005626-471f-4660-bd15-40b72931bd75)

![image](https://github.com/ooonush/Stats/assets/72870405/f96b9c3f-4e90-43ef-8f4b-a9205a45b866)

If you want to set a target version, **Stats** uses the *.*.* release tag so you can specify a version like #0.0.1-pre.1. For example `https://github.com/ooonush/Stats.git?path=Assets/Plugins/Stats#0.0.1-pre.1`.

## Basic Concepts

### Stats
**Stats** are specific numeric traits of a character. These values can be calculated by **formula**, depend on other **stats**, change and **modified** during the game.

For example, a stat can be a `strength`, which depends on the level of the character or the weapon he is wielding.

To create a **Stat**, right click on the project window and go to **Create → Stats → Stat**.

![image](https://github.com/ooonush/Stats/assets/72870405/5ddc159f-5f5b-481d-8766-19de9d507e11)

The value of the stat starts with the **Base** value.
If you specify the **Formula** value, then the final value of the stat will be calculated by this formula.

For example, when creating a strength stat, **Base** can be set to 15. And the **Formula** will increase this value every `level`.

The **Type** value is _required_ for Stat. If it is not specified, the stat _will not be taken into account_.

To create a **Stat Type**, right click on the project window and go to **Create → Stats → StatType**.

![image](https://github.com/ooonush/Stats/assets/72870405/6f9d13c5-dc37-4865-a2ef-ebbf5f406a1d)
![image](https://github.com/ooonush/Stats/assets/72870405/fda985c5-841a-43e4-9ae2-0e2cb6ee152b)

For example, I want to make a different strength value for Archer and Warrior. To do this, first I create a **StatType** `Strength`, then I create a `Warrior Strength`, `Archer Strength` stats and specify **Base** and **Formula** values in them. The **Type** value for both stats is the same - `Strength`.

### Attributes

Attributes are specific numerical traits of a character that have _min_ and _max_ values.

To create a **Attribute**, right click on the project window and go to **Create → Stats → AttributeType**.

![image](https://github.com/ooonush/Stats/assets/72870405/be2678c7-44a4-4fd3-92c9-8eb40d5ff682)

- **MinValue** is the minimum value of the attribute.
- **MaxValue** is the type of stat that will be the maximum value. Unlike **MinValue**, **MaxValue** _can change during the game_.
- **Start Percent** is the percent at which the attribute starts.

A typical example of an **attribute** in games is character `Health`. In this case, we need to specify **MinValue = 0** and create new **Stat** and **StatType** for `MaxHealth`. **Start Percent = 1**, will mean that the character's original `Health` is full.

### Traits Classes

**TraitsClass** contains all **Stats** and **Attributes** of a character. You can have several character classes with different traits (stats and attributes).

To create a **TraitsClass**, right click on the project window and go to **Create → Stats → TraitsClass**.

For example, an **Archer** class might look like this:

![image](https://github.com/ooonush/Stats/assets/72870405/688cad40-3943-4f79-9c5a-d6e4a998b2bd)

When you have many _trait classes_, you may forget to add some **Stat** to one of them (e.g. Strength). Therefore, it is recommended that you use _custom trait classes_. To do this, you must create a script and inherit **TraitsClassBase** to control the traits of the character _using code_. For example, this is what a _custom trait class_ might look like:

```csharp
[CreateAssetMenu(menuName = "Character Class", fileName = "Character Class")]
public class CharacterClass : TraitsClassBase
{
    [SerializeField] private StatItem _maxHealth;
    [SerializeField] private StatItem _strength;

    [SerializeField] private AttributeItem _health;
}
```

This way, you explicitly specify in the code what stats your character has and never forget to add a Stat or Attribute to _traits class_.

![image](https://github.com/ooonush/Stats/assets/72870405/d597e99f-9be5-447d-aaa8-48c7a8dd47db)

### Traits

**Traits** is the _component_ that binds the **TraitsClass** Asset to the GameObject.

![image](https://github.com/ooonush/Stats/assets/72870405/a46fcb4c-b4e9-415e-8079-24a0f1058684)

After adding a component, you can specify the desired traits class through the _Inspector_:

![image](https://github.com/ooonush/Stats/assets/72870405/f6adc780-5ecb-412c-a1ec-b90bc6fee508)

After starting the game, the **Traits** component will be ready to use. The _Inspector_ will show the values of all current **Stats** and **Attributes**. So you can watch the changes in the character traits during the game.

![image](https://github.com/ooonush/Stats/assets/72870405/95114a61-049b-434e-87df-118b9aec1820)

## Formulas

The **formulas** are currently under development, but you can always create your own implementation by inheriting the **StatFormula** class.

## Scripting

### Custom TraitsClass

Custom _traits class_ is useful in large projects where you need to keep track of multiple _trait classes_. When you create a custom **TraitsClass**, you create a kind of _contract_ that allows you to customize characters and add new ones more easily. It's also useful if you want to have control over traits in your code.

To create a custom _trait class_, you need to inherit **TraitsClassBase**:

```csharp
[CreateAssetMenu(menuName = "Character Class", fileName = "Character Class")]
public class CharacterClass : TraitsClassBase
{
    [SerializeField] private StatItem _maxHealth;
    [SerializeField] private StatItem _strength;

    [SerializeField] private AttributeItem _health;
}
```

You can also create lists or arrays of stats and attributes. A good practice is a combination of the two:

```csharp
[CreateAssetMenu(menuName = "Character Class", fileName = "Character Class")]
public class CharacterClass : TraitsClassBase
{
    [SerializeField] private StatItem _maxHealth;
    [SerializeField] private StatItem _strength;

    [SerializeField] private AttributeItem _health;

    [SerializeField] private List<StatItem> _additionalStats;
    [SerializeField] private AttributeItem[] _additionalAttributes;
}
```

### Traits

#### Initialization

To use the **Traits** component, you must specify **TraitsClass**. This can be done with the help of the _Inspector_, or in the code, using the **Initialize()** method.
The best practice is to call **Initialize()** in the **Awake()** method of another class, or immediately after calling **Instantiate()**.

```csharp
public class Character : MonoBehaviour
{
    public Traits Traits;
    public CharacterClass CharacterClass; // This is the custom TraitsClass

    private void Awake()
    {
        Traits.Initialize(CharacterClass);
    }
}
```

```csharp
public class CharacterSpawner : MonoBehaviour
{
    public Traits CharacterPrefab;

    // CharacterClass is the custom TraitsClass
    public Traits CreateCharacter(CharacterClass characterClass)
    {
        Traits characterTraits = Instantiate(CharacterPrefab, transform.position, Quaternion.identity);
        characterTraits.Initialize(characterClass);

        return characterTraits;
    }
}
```

#### RuntimeStats and RuntimeAttributes

**Traits** has **RuntimeStats** and **RuntimeAttributes**, which can be used to get specific _stats_ and _attributes_. To do this, _call_ the **Get()** method in **RuntimeStats** or **RuntimeAttributes** and pass a **StatType** or **AttributeType** object there, respectively.

```csharp
public class Character : MonoBehaviour
{
    [SerializeField] private Traits _traits;

    [SerializeField] private StatType _strengthType;
    [SerializeField] private AttributeType _healthType;

    private RuntimeStat _strength;
    private RuntimeAttribute _health;

    private void Awake()
    {
        _strength = _traits.RuntimeStats.Get(_strengthType); // Getting the Strength RuntimeStat
        _health = _traits.RuntimeAttributes.Get(_healthType); // Getting the Health RuntimeAttribute
    }
}
```

The **Get()** method returns **RuntimeStat** and **RuntimeAttribute**. These are objects that contain information such as the current stat value, the attribute value, stat modifiers, and much more.

#### RuntimeStat

**Value** - The current value of the stat, including formulas and modifiers.
**Base** - The base value of the stat, without formulas and modifiers. You can change this value.
**ModifiersValue** - The value of the modifiers that are added to the **Base** value after the **Formulas**.

#### RuntimeAttribute

**MinValue** - The min value of the attribute.
**MaxValue** - The max value of the attribute. Is the **Value** of **RuntimeStat**
**Value** - The current value of the attribute. You can change this value.
**Ratio** - The Value to **MaxValue** ratio.

#### Modifiers

**Modifiers** are **Constant** or **Percent** numeric values that are added to **Stats** at _runtime_. The **Constant** modifier adds a _fixed_ value to the base value of the stat. And **Percent**, adds a specified _percentage_ to the base value of the stat.

You can add **modifiers** when a character puts on some `armor`, and remove a modifier when a character removes an `armor`.

```csharp
private RuntimeStat _maxHealth;

public void PutArmor(int protection)
{
    _maxHealth.AddModifier(ModifierType.Constant, protection);
}

public void RemoveArmor(int protection)
{
    _maxHealth.RemoveModifier(ModifierType.Constant, protection);
}
```
