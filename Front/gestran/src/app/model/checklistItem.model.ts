export class ChecklistItem {

  constructor(public id?: Number | null,
    public idChecklist?: Number,
    public idItem?: Number,
    public nomeItem?: string,
    public idUsuarioAlteracao?: Number,
    public nomeUsuarioAlteracao?: string,
    public executado?: boolean,
    public observacaoItem?: string,
    public isSelected?: boolean) {
  }
}