import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreOfferInfoPageComponent } from './ire-offer-info-page.component';

describe('IreOfferInfoPageComponent', () => {
  let component: IreOfferInfoPageComponent;
  let fixture: ComponentFixture<IreOfferInfoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreOfferInfoPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreOfferInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
