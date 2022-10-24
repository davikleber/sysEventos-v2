import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  //providers: [EventoService]
})
export class EventosComponent implements OnInit {

  //modalRef: BsModalRef;
  modalRef ={} as BsModalRef;
  public eventos: Evento[] =[];
  public eventosFilter: Evento[] =[];
  public widthImg: number = 130;
  public marginImg: number =2 ;
  public showImg: boolean = true;
  private _filterList: string = '';

  public get filterList(): string{
    return this._filterList;
  }

  public set filterList(value: string){
    this._filterList = value;
    //condição
    this.eventosFilter = this.filterList ? this.filterEventos(this.filterList) : this.eventos
  }

 public filterEventos(filterper:string):Evento[]{
    filterper = filterper.toLocaleUpperCase();
    return this.eventos.filter(
      (evento: { tema: string, local: string }) => evento.tema.toLocaleUpperCase().indexOf(filterper) !== -1 ||
      evento.local.toLocaleUpperCase().indexOf(filterper) !== -1



    );
  }

  constructor(private eventoSerice: EventoService, private modalService: BsModalService) { }

  public ngOnInit(): void {
    this.getEventos();

  }

  public alterImg(): void{
    this.showImg = !this.showImg;
  }



  public getEventos():void{
    this.eventoSerice.getEventos().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFilter = this.eventos;
      },
      error => console.log(error)
    );

  }

  openModal(template: TemplateRef<any>) :void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}

