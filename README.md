# GoalNetShop

GoalNetShop è un'applicazione web per la gestione di un negozio online di articoli sportivi.

## Descrizione

GoalNetShop consente agli utenti di visualizzare e acquistare una varietà di prodotti sportivi, come abbigliamento e calzature. Gli utenti possono aggiungere articoli al carrello e completare l'acquisto.
GoalNetShop consente anche all'utente Admin nonchè proprietario del sito di vedere una lista degli ordini effettuati dai clienti, di aggiungere nuovi articoli, di modficare gli articoli, e eliminazione degli stessi.

## Caratteristiche

Responsive Design: GoalNetShop è progettato con un design responsive che si adatta automaticamente a diverse dimensioni di schermo e dispositivi. Questo assicura un'esperienza utente ottimale sia su desktop che su dispositivi mobili.

## Tecnologie utilizzate

 - Piattaforma: ASP.NET MVC Framework
 - Gestione dei Dati: Entity Framework, SQL Server

## Installazione

1. Clona il repository: `git clone https://github.com/tuonome/goalnetshop.git`
2. Apri il progetto in Visual Studio
3. Configura il database nel file `Web.config`
4. Esegui l'applicazione in modalità debug o pubblicala su un server web

## Configurazione

- Assicurati di configurare correttamente la connessione al database nel file `Web.config`.
- Puoi personalizzare il layout e lo stile dell'applicazione modificando i file CSS e le viste Razor.
- Per funzionare, va aggiunto al database nella tabella "Roles", nel campo "TypeRole" (pecificamente scritti nel seguente modo):
     - Admin
     - User 
- L'admin NON si può registrare. Va aggiunto manualmente al database nella tabella "Users", mettendogli come id nel campo "RoleId" quello corrispondente all'id nella tabella "Roles".

## Utilizzo User

1. Accedi all'applicazione dal browser all'indirizzo `https://localhost:44362`.
2. Registra il tuo profilo ed effettua il login.
2. Esplora i prodotti disponibili e aggiungi quelli desiderati al carrello.
3. Completa l'acquisto tramite il processo di checkout.
4. Accedi alla tua area utente per visuallizare la cronologia dei tuoi ordini o mofificare i prorpi fati utente.

## Utilizzo Admin

1. Accedi all'applicazione dal browser all'indirizzo `https://localhost:44362`.
2. Visualizza una lista di ordini effettuati dai clienti.
3. Aggiungi un nuovo articolo.
4. Modifica un articolo già esistente.
5. Elimina un articolo.

## Contributi

Sii libero di contribuire al progetto mediante suggerimenti, segnalazioni di bug o richieste di nuove funzionalità. Segui le linee guida di GitHub per la collaborazione.

## Contatti

Il progetto GoalNetShop è stato sviluppato da Laveglia Vincenzo.

[GitHub](https://github.com/Vincenzolaveglia) - [LinkedIn](https://www.linkedin.com/in/vincenzo-laveglia-404baa2ab/)


