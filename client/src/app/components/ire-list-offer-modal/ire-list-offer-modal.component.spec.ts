import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreListOfferModalComponent } from './ire-list-offer-modal.component';

describe('IreListOfferModalComponent', () => {
  let component: IreListOfferModalComponent;
  let fixture: ComponentFixture<IreListOfferModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreListOfferModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreListOfferModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
