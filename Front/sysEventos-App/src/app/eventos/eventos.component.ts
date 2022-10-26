import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
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

  constructor(
    private eventoSerice: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();

  }

  public alterImg(): void{
    this.showImg = !this.showImg;
  }



  public getEventos():void{
    this.eventoSerice.getEventos().subscribe({
      next: (eventos: Evento[]) =>{
        this.eventos = eventos;
        this.eventosFilter = this.eventos;
      },
      error: (error: any) => {
        console.log(error),
        this.toastr.error('Erro ao carregar os Eventos', 'Erro!');
      },
      complete:() => this.spinner.hide()
    });

  }

  openModal(template: TemplateRef<any>) :void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('Deletado com sucesso!', 'Deletado!');
  }

  decline(): void {
    this.modalRef?.hide();
  }
}

