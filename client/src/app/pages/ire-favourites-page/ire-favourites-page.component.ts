import {Component, OnInit} from '@angular/core';
import {IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {UserHttpService} from "../../services/user-http.service";

@Component({
  selector: 'app-ire-favourites-page',
  templateUrl: './ire-favourites-page.component.html',
  styleUrls: ['./ire-favourites-page.component.css']
})
export class IreFavouritesPageComponent implements OnInit{

    protected offersList: Array<IOfferResultDto> = new Array<IOfferResultDto>();

    constructor(
        private userHttpService: UserHttpService
    ) {
    }

    async ngOnInit() {
        this.getLikedOffers();
    }

    protected statusChanged = () => {
        this.getLikedOffers();
    }

    private getLikedOffers = () => {
        this.userHttpService.getLikedOffers().subscribe({
            next: (result: Array<IOfferResultDto>) => {
                this.offersList = result;
            },
            error: (error: any) => {
                console.log(error);
            }
        });
    }
}
