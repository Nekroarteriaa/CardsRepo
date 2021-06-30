Magnific Games Frontend Developer Test
--------------------------------------

Task:   Implement the logic for Battledeck UI
Result: A fully functional UI in the Battledeck.unity scene
Format: Send back a zip archive of your Assets, Packages and ProjectSettings folders
Unity: 2019.4.xx (LTS)

We provide a simplified version of War Alliance BattleDeck UI
Your task is to implement the requiered code to support the desired functionality:

- User can scroll through his card collection
- Tapping a card in the collection should display a submenu where the player can choose "Select" to start editing his Deck
- While in 'Edit mode', player can select a card in his active deck to replace it
    (by tapping  it or by drag & dropping the previously selected card over it)
- While in 'Edit mode', tapping elsewhere should cancel the 'Edit mode'

Optional
- User can manage 5 different decks
- User can sort his collection by level, energy cost and rarity

Please add animation/transition feedback as needed.
For simplicity let assume a fixed iPad resolution of 1536x2048

You should use the War Alliance game as a design reference.
https://play.google.com/store/apps/details?id=com.magnificgames.waralliance&hl=en&gl=US

The implementation rely on a MVP design pattern:
https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93presenter

As there is no external model update in the test, you can safely assume the model
is modified only by you through the BattleviewPresenter.