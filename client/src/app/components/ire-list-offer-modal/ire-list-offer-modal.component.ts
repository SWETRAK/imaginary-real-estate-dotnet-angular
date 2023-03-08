import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {IRegisterUserWithPasswordDto} from "../../interfaces/IRegisterUserWithPasswordDto";
import {INewOfferIncomingDto} from "../../interfaces/INewOfferIncomingDto";
import {OfferHttpService} from "../../services/offer-http.service";
import {IOfferResultDto} from "../../interfaces/IOfferResultDto";
import {ImageHttpService} from "../../services/image-http.service";
import {IImageOfferResultDto} from "../../interfaces/IImageOfferResultDto";

@Component({
  selector: 'app-ire-list-offer-modal',
  templateUrl: './ire-list-offer-modal.component.html',
  styleUrls: ['./ire-list-offer-modal.component.css']
})
export class IreListOfferModalComponent implements OnInit {

    @ViewChild('modal', {static: true}) protected modal: ElementRef | undefined;

    @Output("closed") closed = new EventEmitter<boolean>();

    private frontPhoto: File | undefined;
    private additionalPhotos: Array<File> = new Array<File>();

    protected frontLabel: string = "Image not selected";
    protected additionalLabel: string = "Images not selected";

    protected listOfferForm: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private offerHttpService: OfferHttpService,
        private imageOfferService: ImageHttpService
    ) {
        this.listOfferForm = this.formBuilder.group({
            title: new FormControl(null, [
                Validators.required,
                Validators.minLength(1)
            ]),
            address: new FormControl('', [
                Validators.required,
                Validators.minLength(1)
            ]),
            price: new FormControl('', [
                Validators.required,
                Validators.minLength(1)
            ]),
            bedrooms: new FormControl(1, [
                Validators.required,
                Validators.minLength(1)
            ]),
            bathrooms: new FormControl(1, [
                Validators.required,
                Validators.minLength(1)
            ]),
            area: new FormControl('', [
                Validators.required,
                Validators.minLength(1)
            ]),
            description: new FormControl('', [
                Validators.required,
                Validators.minLength(1)
            ]),
            frontPhoto: new FormControl('', [
                Validators.required,
            ]),
            addPhoto: new FormControl(''),
        });
    }

    ngOnInit() {

    }

    protected onFrontFileChange = (event: any) => {
        const files: FileList = event.target.files;
        if (files.length == 1) {
            this.frontPhoto = files[0];
            this.frontLabel = files[0].name;
        }
    }

    protected onAdditionalPhotos = (event: any) => {
        const files: FileList = event.target.files;
        if (files.length > 0) {
            this.additionalPhotos = [];
            this.additionalLabel = "";
            for (let i = 0; i < files.length; i++) {
                let file: File | null = files.item(i);
                if (file != null) {
                    this.additionalPhotos.push(file);
                    this.additionalLabel += file.name + ", ";
                }
            }
        }
    }

    protected createOffer() {
        if (this.listOfferForm.valid) {
            this.listOfferForm.disable();
            const listOfferDto: INewOfferIncomingDto = this.listOfferForm.value;

            this.offerHttpService.createOffer(listOfferDto).subscribe({
                next: (offerResult: IOfferResultDto) => {
                    console.log(offerResult);
                    if (this.frontPhoto != null) {
                        this.uploadPhoto(
                            this.frontPhoto,
                            offerResult.identifier,
                            true,
                            (response: IImageOfferResultDto) => {
                                if (this.additionalPhotos != null) {
                                    for (const file of this.additionalPhotos) {
                                        this.uploadPhoto(file, offerResult.identifier, false, (response: IImageOfferResultDto) => {
                                        });
                                    }
                                    this.close();
                                }
                            });
                    }
                },
                error: (error: any) => {
                    console.log(error);
                    this.close();
                }
            });

        }
    }

    private uploadPhoto = (file: File, offerGuid: string, frontPhoto: boolean, responseFunction: Function) => {
        const frontImageOffer = new FormData();
        frontImageOffer.append("offerGuid", offerGuid);
        frontImageOffer.append("frontPhoto", frontPhoto ? "true" : "false");
        frontImageOffer.append("file", file);
        this.imageOfferService.uploadImage(frontImageOffer).subscribe({
            next: (response: IImageOfferResultDto) => {
                responseFunction(response);
            },
            error: (error: any) => {
                console.log(error);
            },
        });
    }

    protected close = () => {
        this.closed.emit(false);
    }
}

