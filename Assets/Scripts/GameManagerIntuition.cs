using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameManagerIntuition : MonoBehaviour
{
    #region Public&Private Fields
    //Public:
	
    //Private:
	/*

    //I- Initialisations des positions ------------------------------------

    x
    y

    //O - Initialisations des positions -----------------------------------
    //I - Etats tuiles ----------------------------------------------------
    private bool Selected = false; 

    //O - Etats tuiles ----------------------------------------------------


    */
    #endregion
 
    #region Own Methods

    void BoardGeneration(){
        /*
        Question des dimensions:
            prédeterminées pour chaque niveau et/ou choisies par le joueur

        Question de l'attribution coordonnée tableau/tuile
            déterminer nb de tuiles différentes (types)
            combien de tuiles par type

            attribuer position pour tuile > retirer cette tuile de la liste des tuiles possibles
                booléen attribuée ? 
            

        */
    }


	void FlipCards() {
        /*
        rotation de 180° selon z
        */
    }

    void AskQuestion() {
        /*
        Poser la question selon un choix de question
            TYPE de question > prédéterminé au début du niveau
            CONTENU de question > change à chaque fois

        il nous faudrait : liste de questions pré-générées
            autre script ou nouvelle méthode ?

            liste des TYPES : 
            liste des CONTENUS :

        Fenetre qui pose la question ou au moins un petit texte qui s'affiche
            si on veut que le texte soit chouette > mériterait son propre script
        */
    }

    void CardsSelection(){
        /*
        quelque chose qui suivrait les mouvements de la souris et/ou clavier
        dès que souris et/ou clavier au dessus tuile > Highlight()
            flotter 

        si click/enter alors Selected (tuile sélectionnée) > tuile surbrillance
            soulever + flotter moins fort

        */
    }

    void Highlight(){
        /*
        un peu bonus, peut rester dans CardsSelection()
        */
    }

    void CompareSelection(){
        /*
        Comparer coordonnées tableau voulues avec coordonnées tableaux sélectionnées par le joueur
        si comparaison ok > alors niveau validé > Continue + Try Again + Settings + Menu
            si comparaison ok..bcp de code 
                mériterait presque sa fonction à part > BoardGeneration()

        sinon > niveau perdu > menu flottant > Try Again + Settings + Menu
            écran tremblotte

        return booléen
        */
    }

    #endregion

    #region Unity Methods

    void Awake()
    {
        /*
        //I- Initialisations du tableau --------------------------------------- 
        Création d'un tableau dont la taille serait relative au niveau
            > pour chaque niveau, un tableau différent !

        //O- Initialisations du tableau ---------------------------------------   

        */     
    }
    void Start()
    {
        /*
        //I- Initialisations des positions ------------------------------------
        Positionner les tuiles
            coordonnées des tuiles : x, y
            assigner ces coordonnées relativement au tableau dans le Awake()
                mériterait son propre script aussi

        //O- Initialisations des positions ------------------------------------
        */
    }
 
    void Update()
    {
        /*
        //I- Timer ------------------------------------------------------------

            objectif : infos de temps
                > déclancher retournement des tuiles
                > puis poser la question
                > le joueur a un certain temps spour répondre    
        //O- Timer ------------------------------------------------------------

        Dès que timer == tantDeTemps {
            FlipCards();
        }
        Dès que timer == tanDeTempsMaisUnPeuPlusQuandMême {
            AskQuestion();
        }

        Dès que timer == tanDeTempsMaisEncorePlusQuandMême {
            CardsSelection();
        }

        Dès fin timer (ou cliquer sur valider) {
            CompareSelection();
        }

        */
    }
    #endregion
}