import { Item } from "./item.model";

export class Checklist {

  constructor(public id?: Number | null,
    public descricao?: string,
    public statusNome?: string,
    public responsavel?: string,
    public idStatus?: Number,
    public idUsuarioAlteracao?: Number,
    public idUsuarioExecutor?: Number,
    public listaItens?: Item[]) {
  }

}