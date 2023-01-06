import {Component, OnInit} from '@angular/core';
import {LocalStorageService} from "../../services/local-storage.service";
import {Router} from "@angular/router";
import {IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {UserHttpService} from "../../services/user-http.service";

@Component({
  selector: 'app-ire-list-offer-page',
  templateUrl: './ire-list-offer-page.component.html',
  styleUrls: ['./ire-list-offer-page.component.css']
})
export class IreListOfferPageComponent implements OnInit{

    protected modalActivated: boolean = false;
    protected listedOffers: Array<IOfferResultDto> = new Array<IOfferResultDto>();

    constructor(
        private localStorageService: LocalStorageService,
        private router: Router,
        private userHttpService: UserHttpService
    ) {

    }

    ngOnInit() {
        this.userHttpService.getListedOffers().subscribe({
            next: (result) => {
                this.listedOffers = result
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }

    protected removeFromList = (ident: string) => {
        const indexOfRemovedElement = this.listedOffers.findIndex((e) => e.identifier === ident);
        if (indexOfRemovedElement > -1) {
            this.listedOffers.splice(indexOfRemovedElement, 1);
        }
    }

    protected openModal = () => {
        this.modalActivated = true;
    }

    protected closeModal = (closed: boolean) => {
        this.modalActivated = closed;
        this.userHttpService.getListedOffers().subscribe({
            next: (result) => {
                this.listedOffers = result
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }
}
