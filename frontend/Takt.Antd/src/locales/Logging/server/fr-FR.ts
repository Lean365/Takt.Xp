export default {
  logging: {
    server: {
      title: 'Surveillance du serveur',
      refresh: 'Actualiser',
      refreshResult: {
        success: 'Données actualisées avec succès',
        failed: 'Données non actualisées'
      },
      resource: {
        title: 'Ressources système',
        cpu: 'Utilisation CPU',
        memory: 'Utilisation mémoire',
        disk: 'Utilisation disque'
      },
      system: {
        title: 'Informations système',
        os: 'Système d\'exploitation',
        architecture: 'Architecture',
        version: 'Version',
        processor: {
          name: 'Nom du processeur',
          count: 'Nombre de processeurs',
          unit: 'unité'
        },
        startup: {
          time: 'Heure de démarrage',
          uptime: 'Temps d\'exécution'
        }
      },
      dotnet: {
        title: 'Informations de démarrage',
        runtime: {
          title: 'Informations de démarrage',
          directory: 'Répertoire',
          version: 'Version',
          framework: 'Framework'
        },
        clr: {
          title: 'Informations CLR',
          version: 'Version'
        }
      },
      network: {
        title: 'Informations réseau',
        adapter: 'Adaptateur réseau',
        mac: 'Adresse MAC',
        ip:{
          address: 'Adresse IP',
          location: 'Emplacement',
        },
        rate:
        {
          send: 'Envoi',
          receive: 'Réception'
        }
      }
    }
  }
}

