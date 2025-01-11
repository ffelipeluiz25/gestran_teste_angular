export class Item {

  constructor(public id?: Number | null,
    public nome?: string,
    public observacao?: string,
    public idUsuarioAlteracao?: Number,
    public isSelected?: boolean) {
  }

}