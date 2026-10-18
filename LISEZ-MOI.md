# RDVSanté – maquette front-end .NET MAUI

Projet **.NET 8 / .NET MAUI** reproduisant les interfaces de `Mes_interfaces.pdf`
(données de démonstration, aucun back-end).

## Lancer dans Visual Studio Community (Windows)
1. Visual Studio 2022 (17.8 ou plus) avec la charge de travail **« Développement d'applications mobiles avec .NET »** (MAUI).
2. Ouvrir `RdvSante.slnx`.
3. Choisir la cible **Windows Machine** (application non empaquetée : pas besoin du mode développeur) puis **F5**.
   - Pour le rendu mobile : choisir un **émulateur Android** (Android Device Manager de VS) puis **F5**.

## Parcours reliés
Connexion → (Créer un compte) → Accueil (recherche, filtres, états chargement / vide / erreur)
→ Fiche praticien → Choisir un créneau → Vérifier et confirmer → C'est confirmé !
→ Mes rendez-vous → Annuler (confirmation) → Rendez-vous annulé → Historique → Profil (mode sombre, déconnexion).

## Astuces de démonstration
- Taper `erreur` dans la recherche : affiche l'écran « Connexion indisponible ».
- Filtre « Généraliste » + recherche « Dermatologue » : affiche l'écran « Aucun praticien trouvé ».
- Connexion : n'importe quel email valide + un mot de passe non vide.
- Annuler le rendez-vous le déplace dans l'Historique (statut « Annulé »).

## Structure
- `Pages/` : un écran par page (XAML + code-behind)
- `Controls/AppointmentCard` : carte récapitulative réutilisée
- `Services/AppState.cs` : données de démonstration et état partagé ; `Services/Ui.cs` : navigation et validation
- `Resources/Styles` : couleurs et styles (thème clair/sombre) ; `Resources/Images` : photos et icônes d'onglets
