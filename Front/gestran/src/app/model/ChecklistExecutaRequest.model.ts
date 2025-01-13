export class ChecklistExecutaRequest {

  constructor(public id?: Number | null,
    public idStatus?: Number,
    public idUsuarioAlteracao?: Number,
    public listaItens: ChecklistItemExecutaRequest[] = []) {
  }

}

export class ChecklistItemExecutaRequest {

  constructor(public id?: Number | null,
    public executado: boolean = false,
    public isSelected?: boolean) {
  }

}
