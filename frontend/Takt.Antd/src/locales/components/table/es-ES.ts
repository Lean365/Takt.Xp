export default {
  table: {
    viewMode: {
      normal: 'Tabla Tradicional',
      transpose: 'Tabla Transpuesta'
    },
    columns: {
      id: 'ID',
      remark: 'Observación',
      createBy: 'Creado Por',
      createTime: 'Hora de Creación',
      updateBy: 'Actualizado Por',
      updateTime: 'Hora de Actualización',
      deleteBy: 'Eliminado Por',
      deleteTime: 'Hora de Eliminación',
      isDeleted: 'Eliminado',
      operation: 'Operación',
    },
    config: {
      density: {
        default: 'Predeterminado',
        middle: 'Medio',
        small: 'Compacto'
      },
      columnSetting: 'Configuración de Columna'
    },
    pagination: {
      total: 'Total {total} elementos',
      current: 'Página {current}',
      pageSize: '{pageSize} elementos por página',
      jump: 'Ir a'
    },
    empty: 'Sin Datos',
    loading: 'Cargando...',
    selectAll: 'Seleccionar Todo',
    selected: '{total} elementos seleccionados'
  }
}