export default {
  table: {
    viewMode: {
      normal: 'Tableau Traditionnel',
      transpose: 'Tableau Transposé'
    },
    columns: {
      id: 'ID',
      remark: 'Remarque',
      createBy: 'Créé Par',
      createTime: 'Heure de Création',
      updateBy: 'Mis à Jour Par',
      updateTime: 'Heure de Mise à Jour',
      deleteBy: 'Supprimé Par',
      deleteTime: 'Heure de Suppression',
      isDeleted: 'Supprimé',
      operation: 'Opération',
    },
    config: {
      density: {
        default: 'Par Défaut',
        middle: 'Moyen',
        small: 'Compact'
      },
      columnSetting: 'Paramètre de Colonne'
    },
    pagination: {
      total: 'Total {total} éléments',
      current: 'Page {current}',
      pageSize: '{pageSize} éléments par page',
      jump: 'Aller à'
    },
    empty: 'Aucune Donnée',
    loading: 'Chargement...',
    selectAll: 'Tout Sélectionner',
    selected: '{total} éléments sélectionnés'
  }
}