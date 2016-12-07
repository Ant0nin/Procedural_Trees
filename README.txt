====== Techniques implémentées =================================

- Colonisation de l'espace
- Modèle de Borchert Honda
- Agrégation des nouvelles branches avec pondération
- Calcul de la largeur des branches
- Génération procédurale du maillage à partir du squelette

====== Instructions d'utilisation ==============================

Pour des raisons de simplicité nous avons préparé des objets
(GameObjects) dans la scène qui servent à afficher les meshs 
générés par l'outil de génération d'arbre.

Pour tester la génération d'un arbre :
- Ouvrir le projet Unity (version 5.4.3f1 de préférence)
- Dans l'explorateur d'assets, ouvrir le dossier "DataGen"
- Sélectionner, au choix, l'un des fichiers "tree_data_XX"
- Dans l'inspecteur à droite, modifier éventuellement quelques
paramètres puis cliquer sur "Generate tree"

Cela va alors générer un nouveau mesh et l'exporter dans le 
dossier "Meshes". Des changements devraient apparaitre dans 
la scène.

Astuce : Pour plus de clarté, il est possible de désactiver les
objets indésirables de la scène en les sélectionnant, et, dans
l'inspecteur, décocher la case située à gauche du nom du 
GameObject.

====== Echantillons ============================================

Quelques screens sont égalements présentes dans le dossier
"output_sample" (en dehors du projet Unity).