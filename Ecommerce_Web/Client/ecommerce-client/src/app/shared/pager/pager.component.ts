import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {

  @Input() pageSize?:number
  @Input() totalCount?:number
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  onPageChanged(event:any){
    this.pageChanged.emit(event.page);
  }

  ngOnInit(): void {
  }

}
