import { Component } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  
  constructor(private modalService: NgbModal) {}
  
  open(content: any, label: string) {
    this.modalService.open(content, {ariaLabelledBy: label});
  }
}
