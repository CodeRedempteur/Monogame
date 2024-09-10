////////////////////////////////////////////////////////////
/// Propriétaire du programme : Code Redempteur          ///
/// Youtube : https://www.youtube.com/@CodeRedempteur    ///
///////////////////////////////////////////////////////////
using Microsoft.Xna.Framework;
// Utilise le framework XNA, qui fournit des fonctionnalités de base pour le développement de jeux (game loop, input, etc.)

using Microsoft.Xna.Framework.Graphics;
// Utilise les classes de dessin et de manipulation des textures dans XNA (SpriteBatch, Texture2D, etc.)

using Microsoft.Xna.Framework.Input;
// Utilise les classes pour gérer les entrées clavier et manette (GamePad, Keyboard, etc.)

using MonoGame.Extended;
// Utilise MonoGame.Extended, une bibliothèque qui étend les fonctionnalités de MonoGame (dessin de formes géométriques, etc.)

using System.IO;
// Utilise les classes de gestion de fichiers du framework .NET (lecture/écriture de fichiers).

namespace TutoMonogameSnapshot
// Déclare un espace de noms appelé TutoMonogameSnapshot.

{
    public class Game1 : Game
    // Déclare une classe Game1 qui hérite de la classe Game, la base pour créer un jeu MonoGame/XNA.

    {
        private GraphicsDeviceManager _graphics;
        // Gestionnaire pour configurer et manipuler les paramètres graphiques.

        private SpriteBatch _spriteBatch;
        // SpriteBatch est utilisé pour dessiner des textures sur l'écran.

        public Game1()
        // Constructeur de la classe Game1.

        {
            _graphics = new GraphicsDeviceManager(this);
            // Initialise le gestionnaire graphique avec l'instance actuelle du jeu.

            Content.RootDirectory = "Content";
            // Définit le répertoire racine pour le chargement des contenus (images, sons, etc.).

            IsMouseVisible = true;
            // Rend le curseur de la souris visible dans le jeu.
        }

        protected override void Initialize()
        // Méthode appelée pour initialiser les ressources non graphiques ou les variables avant le chargement du contenu.

        {
            base.Initialize();
            // Appelle la méthode Initialize de la classe parente pour s'assurer que les initialisations de base se font correctement.
        }

        protected override void LoadContent()
        // Méthode appelée pour charger les ressources graphiques comme les textures ou les polices.

        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Initialise le SpriteBatch qui sera utilisé pour dessiner les sprites/textures.
        }

        protected override void Update(GameTime gameTime)
        // Méthode appelée à chaque frame pour mettre à jour la logique du jeu (entrées utilisateur, mouvements, etc.).

        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Si le bouton "Retour" de la manette ou la touche "Echap" est pressée, quitte le jeu.

            base.Update(gameTime);
            // Appelle la méthode Update de la classe parente pour mettre à jour les autres composants du jeu.
        }

        protected override void Draw(GameTime gameTime)
        // Méthode appelée à chaque frame pour dessiner les éléments graphiques à l'écran.

        {
            RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferWidth);
            // Crée un RenderTarget2D, qui est une surface de rendu sur laquelle on peut dessiner avant d'afficher l'image finale.

            GraphicsDevice.SetRenderTarget(renderTarget);
            // Définit le render target pour dessiner dessus.

            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Efface l'écran (ou la cible de rendu) en le remplissant d'une couleur bleu clair.

            _spriteBatch.Begin();
            // Démarre une nouvelle session de dessin avec le SpriteBatch.

            _spriteBatch.DrawRectangle(new Rectangle(0, 0, 300, 300), Color.Black);
            // Dessine un rectangle noir de 300x300 pixels à la position (0, 0).

            _spriteBatch.End();
            // Termine la session de dessin.

            GraphicsDevice.SetRenderTarget(null);
            // Remet le render target par défaut pour redessiner sur l'écran.

            SaveScreenshot(renderTarget);
            // Sauvegarde le contenu de la surface de rendu (le screenshot) dans un fichier image.

            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Efface de nouveau l'écran avec une couleur bleu clair.

            renderTarget.Dispose();
            // Libère les ressources utilisées par le render target.

            _spriteBatch.Begin();
            // Démarre une nouvelle session de dessin.

            _spriteBatch.DrawRectangle(new Rectangle(0, 0, 300, 300), Color.Black);
            // Redessine le rectangle noir de 300x300 pixels à la position (0, 0).

            _spriteBatch.End();
            // Termine la session de dessin.

            base.Draw(gameTime);
            // Appelle la méthode Draw de la classe parente pour dessiner d'autres composants du jeu.
        }

        private void SaveScreenshot(RenderTarget2D renderTarget)
        // Méthode pour sauvegarder le contenu du render target dans un fichier PNG.

        {
            Color[] data = new Color[renderTarget.Width * renderTarget.Height];
            // Crée un tableau pour stocker la couleur de chaque pixel du render target.

            renderTarget.GetData(data);
            // Remplit le tableau avec les données de couleur du render target.

            Texture2D texture = new Texture2D(GraphicsDevice, renderTarget.Width, renderTarget.Height);
            // Crée une nouvelle texture avec les dimensions du render target.

            texture.SetData(data);
            // Applique les données de couleur (pixels) à la texture.

            using (Stream stream = File.Create("screenshot.png"))
            // Crée un fichier PNG appelé "screenshot.png" pour sauvegarder l'image.

            {
                texture.SaveAsPng(stream, texture.Width, texture.Height);
                // Sauvegarde la texture sous forme de PNG dans le fichier.
            }

            texture.Dispose();
            // Libère les ressources utilisées par la texture.
        }
    }
}
